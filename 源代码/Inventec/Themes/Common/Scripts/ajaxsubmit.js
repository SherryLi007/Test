//dom加载完成后执行的js
; $(function () {
    //ajax get请求
    $('.ajax-get').click(function () {
        var target;
        var that = this;
        if ($(this).hasClass('confirm')) {
            if (!confirm('确认要执行该操作吗?')) {
                return false;
            }
        }
        if ($(this).attr('href') != undefined) {
            target = $(this).attr('href');
        }
        else if ($(this).attr('url') != undefined) {
            target = $(this).attr('url');
        }
        {
            $.get(target).success(function (data) {
                if (data.status == 1) {
                    if (data.url) {
                        updateAlert(data.info, 'success');
                    } else {
                        updateAlert(data.info, 'success');
                    }
                    setTimeout(function () {
                        if (data.url) {
                            location.href = data.url;
                        } else {
                            location.reload();
                        }
                    }, 1500);
                } else {
                    updateAlert(data.info);
                    setTimeout(function () {
                        if (data.url) {
                            location.href = data.url;
                        } else {
                            //$('#top-alert').find('button').click();
                        }
                    }, 1500);
                }
            });

        }
        return false;
    });

    //ajax post submit请求
    $('.ajax-post').click(function () {
        var ajaxform = $(this);

        if (ajaxform.hasClass("checkform")) {
            if (checkform() == false)
                return false;
        }
        if (ajaxform.hasClass("savecheckform")) {
            if (savecheckform() == false)
                return false;
        }

        if (ajaxform.hasClass("btnapprove"))
        {
            $(".approveremark").removeClass("required");
        }
        if (ajaxform.hasClass("btnreject")) {
            $("input").removeClass("required");
            $("select").removeClass("required");
            $(".approveremark").addClass("required");
        }

        if ($(this).hasClass('confirm')) {
            noty({
                text: '<i class="icon-question-sign icon-large"></i> 确认要执行该操作吗? / Are you sure?',
                layout: 'center',
                theme: 'relax',
                modal: true,
                buttons: [
                    {
                        addClass: 'btn btn-primary btn-sm', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                            AjaxSubmitForm(ajaxform);
                        }
                    },
                    {
                        addClass: 'btn btn-danger btn-sm', text: 'Cancel', onClick: function ($noty) {
                            $noty.close();
                            return false;
                        }
                    }
                ]
            });
        }
        else {
            AjaxSubmitForm($(this));
        }
              
        return false;
    });

    function AjaxSubmitForm(ajaxform)
    { 
        var target, query, form, checkvalidator;
        var target_form = ajaxform.attr('target-form');

        checkvalidator = true;
        if (target_form != null && !ajaxform.hasClass('novalid')) {
            var validator = $('.' + target_form).validate();
            checkvalidator = validator.form();
        }
        if (checkvalidator) {

            var that = ajaxform;
            var nead_confirm = false;
            if (ajaxform.attr('href') != undefined) {
                target = ajaxform.attr('href');
            }
            else if (ajaxform.attr('url') != undefined) {
                target = ajaxform.attr('url');
            }
            {
                if (target_form != undefined) {
                    form = $('.' + target_form);
                    if (ajaxform.attr('hide-data') === 'true') {//无数据时也可以使用的功能
                        form = $('.hide-data');
                        query = form.serialize();
                    } else if (form.get(0) == undefined) {
                        return false;
                    } else if (form.get(0).nodeName == 'FORM' && target == undefined) {
                        if (ajaxform.attr('url') !== undefined) {
                            target = ajaxform.attr('url');
                        } else {
                            target = form.get(0).action;
                        }
                        query = form.serialize();
                    } else if (form.get(0).nodeName == 'INPUT' || form.get(0).nodeName == 'SELECT' || form.get(0).nodeName == 'TEXTAREA') {
                        form.each(function (k, v) {
                            if (v.type == 'checkbox' && v.checked == true) {
                                nead_confirm = true;
                            }
                        })
                        query = form.serialize();
                    } else {
                        query = form.find('input,select,textarea').serialize();
                    }
                }
                else {
                    query = "";
                }

                ajaxform.addClass('disabled');

                var loading = noty({
                    layout: 'center',
                    type: 'Alert',
                    text: '<i class="icon-spinner icon-spin"></i> Waiting...',
                    theme: 'relax',
                    animation: {
                        open: { height: 'toggle' },
                        close: { height: 'toggle' },
                        easing: 'swing',
                        speed: 500
                    },
                });

                $.post(target, query).success(function (data) {
                    //loading.close();
                    if (data.status == 1) {
                        if (data.url) {
                            loading.setText(data.info);
                            loading.setType("success");
                        } else {
                            loading.setText(data.info);
                            loading.setType("success");
                        }
                        setTimeout(function () {
                            loading.close();
                            if (data.url) {
                                if (data.url == "close") {
                                    $(window)[0].opener.location.reload(true);
                                    $(window)[0].close();
                                }
                                else {
                                    location.href = data.url;
                                }
                            } else if ($(that).hasClass('refresh')) {
                                location.reload();
                                $(that).removeClass('disabled').prop('disabled', false);
                            } else {
                                //location.reload();
                                $(that).removeClass('disabled').prop('disabled', false);
                            }
                        }, 2500);
                    } else {
                        loading.setText(data.info);
                        loading.setType("error");
                        setTimeout(function () {
                            loading.close();
                            if (data.url) {
                                location.href = data.url;
                            } else {
                                $(that).removeClass('disabled').prop('disabled', false);
                            }
                        }, 2500);
                    }
                }).error(function () {
                    loading.setText("System Error! 系统错误! ");
                    loading.setType("error");
                    setTimeout(function () {
                        loading.close();
                        $(that).removeClass('disabled');
                    }, 2500);
                });

            }
        }
        else
        {
            updateAlert("Please input message / 请输入必输信息");
        }

    }

    window.updateAlert = function (text, c) {
        text = text || 'default';
        c = c || 'error';
        if (text != 'default') {
            var n = noty({
                layout: 'bottom',
                type: c,
                text: text,
                theme: 'relax',
                timeout: 10000,
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500
                }
            });
        }
    };

    $.ajaxSetup({ cache: false });
});

Number.prototype.toFixed = function (s) {
    changenum = (parseInt(this * Math.pow(10, s) + 0.5) / Math.pow(10, s)).toString();
    index = changenum.indexOf(".");
    if (index < 0 && s > 0) {
        changenum = changenum + ".";
        for (i = 0; i < s; i++) {
            changenum = changenum + "0";
        }
    } else {
        index = changenum.length - index;
        for (i = 0; i < (s - index) + 1; i++) {
            changenum = changenum + "0";
        }
    }
    return changenum;
}

