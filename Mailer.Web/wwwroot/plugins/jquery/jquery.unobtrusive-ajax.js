// Unobtrusive Ajax support library for jQuery
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// @version <placeholder>
//
// Microsoft grants you the right to use these script files for the sole
// purpose of either: (i) interacting through your browser with the Microsoft
// website or online service, subject to the applicable licensing or use
// terms; or (ii) using the files as included with a Microsoft product subject
// to that product's license terms. Microsoft reserves all other rights to the
// files not expressly granted by Microsoft, whether by implication, estoppel
// or otherwise. Insofar as a script file is dual licensed under GPL,
// Microsoft neither took the code under GPL nor distributes it thereunder but
// under the terms set out in this paragraph. All notices and licenses
// below are for informational purposes only.

/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: false */
/*global window: false, jQuery: false */

(function ($) {
    var data_click = "unobtrusiveAjaxClick",
        data_target = "unobtrusiveAjaxClickTarget",
        data_validation = "unobtrusiveValidation";

    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    }

    function isMethodProxySafe(method) {
        return method === "GET" || method === "POST";
    }

    function asyncOnBeforeSend(xhr, method) {
        if (!isMethodProxySafe(method)) {
            xhr.setRequestHeader("X-HTTP-Method-Override", method);
        }
    }

    function hasContentType(contentType) {
        return contentType.indexOf(contentType) !== -1;
    }

    function asyncOnFailure(element, xhr, status) {
        if (xhr.getResponseHeader('location')) {
            window.location.href = xhr.getResponseHeader('location');
        }
        else if (xhr.getResponseHeader('content-type').indexOf('application/json') !== -1) {
            $(document).Toasts('create', {
                title: xhr.responseJSON.code,
                body: xhr.responseJSON.message,
                class: 'bg-danger'
            });
        }
        else {
            alert('Unexpected error occured.');
        }
    }

    function asyncOnSuccess(element, data, xhr) {
        var mode;
        var contentType = xhr.getResponseHeader("Content-Type") || "text/html";

        console.log("Opa dosao success");
        if (contentType.indexOf("application/x-javascript") !== -1) {  // jQuery already executes JavaScript for us
            return;
        }

        // Check if resubmit attribute is used
        var $resubmitElement = $(element.getAttribute("data-resubmit-element") || "");
        if ($resubmitElement.length && $resubmitElement.is('form') && data.success) {
            $resubmitElement.submit();
            return;
        }

        if (data.success && data.redirectUrl) {
            window.location.href = data.redirectUrl;
        }

        if (data.success) {
            return;
        }

        // Check if return type is text/html and then proceed with content update
        if (contentType.indexOf("text/html") !== -1) {
            mode = (element.getAttribute("data-ajax-mode") || "").toUpperCase();
            $(element.getAttribute("data-ajax-update")).each(function (i, update) {
                var top;

                switch (mode) {
                    case "BEFORE":
                        $(update).prepend(data);
                        break;
                    case "AFTER":
                        $(update).append(data);
                        break;
                    case "REPLACE-WITH":
                        $(update).replaceWith(data);
                        break;
                    default:
                        $(update).html(data);
                        break;
                }
            });
        }
        else {
            console.error("Invalid AJAX response data.");
        }
    }

    // Make asyncRequest method available outside of this module
    $.fn.asyncRequest = function (options) {
        $(this).html($("<div class='text-center'></div>")
            .addClass("overlay p-3").append($("<i></i>").addClass("fa fa-2x fa-refresh fa-spin text-primary")));
        asyncRequest(this[0], options);
        return this;
    };

    function asyncRequest(element, options) {
        var confirm, loading, method, duration;

        confirm = element.getAttribute("data-ajax-confirm");
        if (confirm && !window.confirm(confirm)) {
            return;
        }

        loading = $(element.getAttribute("data-ajax-loading"));
        duration = parseInt(element.getAttribute("data-ajax-loading-duration"), 10) || 0;

        $.extend(options, {
            type: element.getAttribute("data-ajax-method") || undefined,
            url: element.getAttribute("data-ajax-url") || undefined,
            cache: (element.getAttribute("data-ajax-cache") || "").toLowerCase() === "true",
            beforeSend: function (xhr) {
                var result;
                asyncOnBeforeSend(xhr, method);
                result = getFunction(element.getAttribute("data-ajax-begin"), ["xhr"]).apply(element, arguments);
                if (result !== false) {
                    loading.show(duration);
                }
                return result;
            },
            complete: function () {
                loading.hide(duration);
                getFunction(element.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(element, arguments);
            },
            success: function (data, status, xhr) {
                asyncOnSuccess(element, data, xhr);               
                getFunction(element.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(element, arguments);
            },
            global: false,
            error: function (xhr, status, error) {
                asyncOnFailure(element, xhr, status);
                getFunction(element.getAttribute("data-ajax-failure"), ["xhr", "status", "error"]).apply(element, arguments);
            }
        });

        options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });

        method = options.type.toUpperCase();
        if (!isMethodProxySafe(method)) {
            options.type = "POST";
            options.data.push({ name: "X-HTTP-Method-Override", value: method });
        }

        // change here:
        // Check for a Form POST with enctype=multipart/form-data
        // add the input file that were not previously included in the serializeArray()
        // set processData and contentType to false
        var $element = $(element);
        if ($element.is("form") && $element.attr("enctype") === "multipart/form-data") {
            var formdata = new FormData();
            $.each(options.data, function (i, v) {
                formdata.append(v.name, v.value);
            });
            $("input[type=file]", $element).each(function () {
                var file = this;
                $.each(file.files, function (n, v) {
                    formdata.append(file.name, v);
                });
            });
            $.extend(options, {
                processData: false,
                contentType: false,
                data: formdata
            });
        }
        // end change

        $.ajax(options);
    }

    function validate(form) {
        var validationInfo = $(form).data(data_validation);
        return !validationInfo || !validationInfo.validate || validationInfo.validate();
    }

    $(document).on("click", "a[data-ajax=true]", function (evt) {
        evt.preventDefault();
        asyncRequest(this, {
            url: this.href,
            type: "GET",
            data: []
        });
    });

    $(document).on("click", "form[data-ajax=true] input[type=image]", function (evt) {
        var name = evt.target.name,
            target = $(evt.target),
            form = $(target.parents("form")[0]),
            offset = target.offset();

        form.data(data_click, [
            { name: name + ".x", value: Math.round(evt.pageX - offset.left) },
            { name: name + ".y", value: Math.round(evt.pageY - offset.top) }
        ]);

        setTimeout(function () {
            form.removeData(data_click);
        }, 0);
    });

    $(document).on("click", "form[data-ajax=true] :submit", function (evt) {
        var name = evt.currentTarget.name,
            target = $(evt.target),
            form = $(target.parents("form")[0]);

        form.data(data_click, name ? [{ name: name, value: evt.currentTarget.value }] : []);
        form.data(data_target, target);

        setTimeout(function () {
            form.removeData(data_click);
            form.removeData(data_target);
        }, 0);
    });

    $(document).on("submit", "form[data-ajax=true]", function (evt) {
        var clickInfo = $(this).data(data_click) || [],
            clickTarget = $(this).data(data_target),
            isCancel = clickTarget && (clickTarget.hasClass("cancel") || clickTarget.attr('formnovalidate') !== undefined);
        evt.preventDefault();
        if (!isCancel && !validate(this)) {
            return;
        }        

        asyncRequest(this, {
            url: clickTarget && clickTarget.attr('formaction') || this.action,
            type: this.method || "GET",
            data: clickInfo.concat($(this).serializeArray())
        });
    });

    // Global error handler
    $(document).ajaxError(function (event, xhr, settings, thrownError) {
        if (xhr.getResponseHeader('location')) {
            window.location.href = xhr.getResponseHeader('location');
        }
        else if (xhr.getResponseHeader('content-type').indexOf('application/json') !== -1) {
            var response = $.parseJSON(xhr.responseText);

            $(document).Toasts('create', {
                title: response.code,
                body: response.message,
                class: 'bg-danger'
            });
        }
        else {
            alert('Unexpected error occured.');
        }
    });
}(jQuery));