$(function () {
    //明细新增
    $(".addbtn").click(function () {
        var thisbtn = $(this);
        $(".needed").addClass("required");
        if ($(this).hasClass("before")) {
            BeforeAction();
        }
        var checkvalidator = true;
        var validator = $(".Form").validate();
        for (i = 0; i < $(".detailinput").length; i++) {
            if (!validator.element($(".detailinput").eq(i))) {
                checkvalidator = false;
            };
        }
        var strprocessname = $("#processname").val();
        if (strprocessname == "Proc_CommonExpense") {
            $('#currency').attr("disabled", false)
            $("#fxrate").attr("disabled", false)
            $(".TaxiDetails").hide();
            $("#lbcnyamout").html("");
        }
        if (strprocessname == "Proc_InvoicePaymentVerification") {
            //当必填项未填时去判断是否选中
            if ($("#expensetype").val() == "" || $("#currency").val() == "" || $("#amount").val() == "" || $("#invoiceno").val() == "") {
                if ($("#closepo").is(":checked")) {
                    $("#closepo").attr("checked", true);
                    $("#closepo").val("true");
                    $("#po").val("true");
                }
            } else {
                $("#closepo").attr("checked", false);
            }

        };


        if (checkvalidator) {

            var secondType = $("#secondaryexpensetype").find("option:selected").text();

            if (secondType.indexOf("Allowances") > -1 || secondType.indexOf("出差补贴") > -1) {
                $("#amount").attr("disabled", false);
                $('#currency').attr("disabled", false)
                $("#fxrate").attr("disabled", false)
                $(".TaxiDetails").hide();
            }
            if (secondType.indexOf("Taxi Cost") > -1 || secondType.indexOf("出租车费") > -1) {
                $("#amount").attr("disabled", false);
                $('#currency').attr("disabled", false);
                $("#fxrate").attr("disabled", false);
            }
            $(".detailtable tbody").append("<tr></tr>");
            for (i = 0; i < $(".detailtable tr").eq(0).find("th").length - 1; i++) {
                var detailinput = $(".detailinput").eq(i);
                if (detailinput.is("select"))
                    $(".detailtable tbody tr:last").append("<td>" + detailinput.children('option:selected').text() + "<input type='hidden' name='item." + (detailinput.attr("name") == "traveltypesel" ? "traveltype" : detailinput.attr("name")) + "' value=\"" + detailinput.children('option:selected').text() + "\"><input type='hidden' name='item." + (detailinput.attr("name") == "travelrequestno" ? "travelrequest" : (detailinput.attr("name") == "traveltypesel" ? "traveltype" : detailinput.attr("name"))) + "id' value=\"" + detailinput.val() + "\"></td>");
                else
                    $(".detailtable tbody tr:last").append("<td>" + detailinput.val() + "<input type='hidden' name='item." + detailinput.attr("name") + "' value=\"" + detailinput.val() + "\"></td>");
            }

            $(".detailtable tbody tr:last").append("<td><i class='icon-pencil icon-large editbtn'></i>&nbsp;&nbsp;&nbsp;<i class='icon-trash icon-large delbtn'></i></td>");

            if (strprocessname == "Proc_TravelExpense") {
                $("#explanation").removeClass("required");
                $("#lbcnyamout").html("");
                var costelement = $("#costelement").val();
                var sectext = $("#secondaryexpensetype").children("option:selected").text();
                if ((sectext.indexOf("Hotel Cost") > -1 || sectext.indexOf("酒店") > -1) && $("#isvat").val() == "1") {
                    var secondaryexpensetype = $("#secondaryexpensetype").val();
                    //  var Invoicenumber = $("#Invoicenumber").val(); 
                    var vatclonre = $(".detailtable tbody tr:last").clone(true);
                    //vatclonre.find("td").eq(3).find("input").val("261200");
                    var explanation = $("#explanation").val();
                    var isvat = $("#isvat").val();
                    var vatvalue = $("#vat").val();

                    vatclonre.find("td").eq(1).text("Hotel Cost VAT");
                    vatclonre.find("td").eq(1).append("<input type='hidden' value='Hotel Cost VAT' name='item.secondaryexpensetype'>");
                    vatclonre.find("td").eq(1).append("<input type='hidden' value='" + secondaryexpensetype + "' name='item.secondaryexpensetypeid'>");
                    vatclonre.find("td").eq(3).text("261200");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + 261200 + "' name='item.costelement'>");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + isvat + "' name='item.isvat' class='isvat'>");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + vatvalue + "' name='item.tax' class='tax'>");
                    // vatclonre.find("td").eq(3).append("<input type='hidden' value='" + Invoicenumber + "' name='item.invoicenumber'>");
                    vatclonre.find("td").eq(12).text("");
                    var vatamount = $("#amount").val();
                    var rate = vatclonre.find("td").eq(2).find("input").eq(1).val();
                    if (vatvalue != "") {
                        var vatamuont = vatamount * vatvalue;
                    }
                    else { var vatamuont = vatamount * rate; }
                    vatclonre.find("td").eq(4).val(vatamuont);
                    vatclonre.find("td").eq(4).find("input").val(vatamuont);
                    var currencyid = vatclonre.find("td").eq(2).find("input").eq(1).val();
                    if (currencyid == $("#BaseCurrencyID").val()) {
                        var LocalCurrency = (1 * vatamuont).toFixed(2);
                        vatclonre.find("td").eq(8).text($.formatMoney(LocalCurrency, 2));
                        vatclonre.find("td").eq(8).append("<input type='hidden' value='" + LocalCurrency + "' name='item.cnyamount'>");
                        AfterAction();
                    }
                    else {
                        $.post("/Base/getcurrency", {
                            currencyid: currencyid,
                        }, function (data, status) {
                            var curr = data;
                            var LocalCurrency = (curr * vatamuont).toFixed(2);
                            vatclonre.find("td").eq(8).text($.formatMoney(LocalCurrency, 2));
                            vatclonre.find("td").eq(8).append("<input type='hidden' value='" + LocalCurrency + "' name='item.cnyamount'>");
                            AfterAction();
                        });
                    }
                    vatclonre.find("td").eq(11).text("");
                    vatclonre.find("td").eq(11).append("<input type='hidden' name='item.explanation' value='' />")
                    $(".detailtable tbody").append(vatclonre);

                    $(".isvat").hide();
                    $(".vat").hide();
                    $("#isvat").attr("checked", false);
                };
                $(".detailtable tbody tr").each(function () {
                    var td = $(this).find("td").eq(3);
                    var trvitd = $(this).find("td").eq(9);
                    var typename = $(this).find("td").eq(0).find("input").eq(0).val();

                    if (td.find("input[name='item.isvat']").length <= 0) {
                        td.append("<input type='hidden' value='" + "" + "' name='item.tax'class='tax'>");
                        td.append("<input type='hidden' value='" + "" + "' name='item.isvat' class='isvat'>");
                    }

                    //添加空的隐藏域   填充空值
                    if (!(typename.indexOf("Hotel Cost") > -1 || typename.indexOf("住宿") > -1)) {

                        if (trvitd.find("input[name='item.travelrequestno']").length <= 0) {
                            trvitd.append("<input type='hidden' value='" + "" + "' name='item.travelrequestid' class='istravelrequestid'>");
                            trvitd.append("<input type='hidden' value='" + "" + "' name='item.travelrequestno'>");
                        }
                    }
                });
                AfterAction();

                $("#isvat").val(0);
                $("#isvat").prop("checked", false);
                $(".TaxiDetails").hide();
                $(".AllowanceDetails").hide();
            }


            if (strprocessname == 'Proc_InvoicePaymentVerification') {
                AfterAction();

                var invoice = $("#invoiceid").val();
                var fil = $("#filname").val();
                var invos = $(".newdetailtable tbody tr:last").find("td").eq(6);
                var invoiceno = invos.find("input").eq(0).val();
                var expensetypeid = $("#expensetypeid").val();
                var poid = $("#poid").val();
                invos.html("");
                invos.append((fil != "" ? "<a href='" + "/Uploads" + fil + "'target='_blank'>" + invoiceno + "</a>" : invoiceno) + "<input type='hidden' value='" + fil + "' name='item.filname'>" + "</a><input type='hidden' value='" + invoiceno + "' name='item.invoiceno'>" + "<input type='hidden' value='" + invoice + "' name='item.invoiceid'>" + "");
                var Expensetype = $(".newdetailtable tbody tr:last").find("td").eq(1);
                Expensetype.append("<input type='hidden' value='" + expensetypeid + "' name='item.expensetypeid'>");
                var poidCol = $(".newdetailtable tbody tr:last").find("td").eq(8);
                poidCol.append("<input type='hidden' value='" + poid + "' name='item.poid'>");

            }

            $(".detailinput").val("");

            $("#currency").val($("#baseCurrencyId").val());
            $("#fxrate").val(1);

            if (strprocessname == "Proc_TravelRequest") {
                //var costcenterid = $("#costcenterid").val();
                //$(".detailtable tbody tr:last").find("td").eq(6).append("<input type='hidden' value='" + costcenterid + "' name='item.costcenterid'>");
                var hiddencostcenterid = $("#hiddencostcenterid").val();
                $("#costcenter").val(hiddencostcenterid);
            }
            $(".detailtable .editbtn").bind("click", function () {

                $(".detailinput").val("");
                var tr = $(this).parent().parent();
                tr.parent().find("tr").removeClass("current");
                tr.addClass("current");
                for (i = 0; i < tr.find("td").length - 1; i++) {
                    var td = tr.find("td").eq(i);
                    var detailinput = $(".detailinput").eq(i);
                    if (detailinput.is("select")) {
                        for (var j = 0; j < detailinput.get(0).options.length ; j++) {
                            if (detailinput.get(0).options[j].text == td.find("input").eq(0).val() || detailinput.get(0).options[j].text == td.find("input").eq(1).val()) {
                                detailinput.get(0).options[j].selected = true;
                                break;
                            }
                        }
                    }
                    else {
                        if (td.find("input").length > 1) {

                            if (td.find("input[name='item.expensetypeid']").length > 0 || td.find("input[name='item.poid']").length > 0) {
                                detailinput.val(td.find("input").eq(0).val());
                            } else {
                                detailinput.val(td.find("input").eq(1).val());
                            }

                        }
                        else {

                            detailinput.val(td.find("input").eq(0).val());
                            if ($(".detailinput").eq(i).prev().is("span")) {
                                $(".detailinput").eq(i).prev().html(detailinput.val());
                            }
                        }

                    }
                }

                var strprocessname = $("#processname").val();
                if (strprocessname == "Proc_TravelExpense") {

                    $("#lbcnyamout").html("");
                    var parExpensesid = $(this).parent().parent().find($('input[name="item.primaryexpensetypeid"]')).val();
                    $("#primaryexpensetype").val(parExpensesid);
                    var sectext = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
                    var sectextid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
                    var praim = $(this).parent().parent().find($('input[name="item.primaryexpensetype"]')).val();
                    var sectext = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
                    var sectextid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
                    var cny = $(this).parent().parent().find($('input[name="item.cnyamount"]')).val();
                    var cost = $(this).parent().parent().find($('input[name="item.costelement"]')).val();
                    var secondaryexpensetypeid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
                    var itemtravelrequest = $(this).parent().parent().find($('input[name="item.travelrequest"]')).val();
                    var itemcurrencyid = $(this).parent().parent().find($('input[name="item.currencyid"]')).val();
                    var isvat = $(this).parent().parent().next().find($('input[name="item.isvat"]')).val();
                    $("#costelement").val(cost);
                    var money = $.formatMoney(cny, 2);
                    $("#cnyamount").val(cny);
                    $("#lbcnyamout").html(money);
                    var secondaryText = $(this).parent().parent().find('input[name="item.secondaryexpensetype"]').val();

                    if (secondaryText.indexOf("Hotel Cost") > -1 || secondaryText.indexOf("酒店") > -1) {

                        if (isvat == "1") {
                            $("#isvat").prop("checked", true);
                            $("#isvat").val(1);
                            $("#vat").show();

                            $(this).parent().parent().next().remove();
                        }

                        $(".isvat").show();
                        $("#travel").addClass("detailinput");
                        $("#istraveno").removeClass("detailinput");
                        var traveldd = $("#travel").find("option").eq(0).val();
                        $("#travel").show();

                        var thisRowTravel = $(this).parent().parent().find("td").eq(9).find("input[name='item.travelrequestid']").val();
                        $("#travel").children("option[value='" + thisRowTravel + "']").attr("selected", true);

                        $("#travel").trigger("change");

                        var tramount = $("#tramount").val();
                        var cha = cny - tramount;
                        if (cha > 0) {
                            $("#explanation").addClass("required");
                        }
                        else { $("#explanation").removeClass("required"); }
                    }
                    else {

                        $("#travel").hide();
                        $("#istraveno").addClass("detailinput");
                        $("#travel").removeClass("detailinput");
                    }
                    $.post("/FormTravelExpense/GetExpenseList", { parentcode: praim }, function (data, status) {
                        $("#secondaryexpensetype").html(data);
                        $("#secondaryexpensetype").children('option:selected').val(sectextid);
                        $("#secondaryexpensetype").children('option:selected').text(sectext);
                    });
                    $("#secondaryexpensetype").val(sectextid);
                    //差旅费用的如果当前行是Taxi，那么显示 taxibtn 
                    if (sectext.indexOf("Taxi") > -1 || sectext.indexOf("出租") > -1) {
                        $('#currency').attr("disabled", "true");
                        $('#primaryexpensetype').attr("disabled", true);
                        $('#secondaryexpensetype').attr("disabled", true);
                        $(".ExchangerateSearch").unbind("click");
                        $('#amount').attr("disabled", "true");
                        $('#fxrate').attr("disabled", "true");
                        gettaxi();
                        //弹框出租车页面
                    }
                    else {
                        if (sectext.indexOf("Allowances") > -1 || sectext.indexOf("出差补贴") > -1) {
                            $('#currency').attr("disabled", "true");
                            $('#primaryexpensetype').attr("disabled", true);
                            $('#secondaryexpensetype').attr("disabled", true);
                            $("#addorg").unbind("click");
                            $('#amount').attr("disabled", "true");
                            $('#fxrate').attr("disabled", "true");
                            getallowance();
                            //弹框出津贴页面
                        }
                        else {
                            $('#currency').attr("disabled", false);
                            $('#primaryexpensetype').attr("disabled", false);
                            $('#secondaryexpensetype').attr("disabled", false);
                            $('#amount').attr("disabled", false);
                            $('#fxrate').attr("disabled", false);
                            if (itemcurrencyid == 1) {
                                $("#fxrate").attr("disabled", true);
                            }
                            else { $("#fxrate").attr("disabled", false); }
                            $(".ExchangerateSearch").bind("click", function () {
                                var input = $(this).prev();
                                var inputid = $(this).next();
                                var curry = $("#currency").val();
                                var curryy = $("#currency");
                                dialog({
                                    id: 'rate-dialog',
                                    title: 'Exchangerate/汇率信息',
                                    url: "/SelectPage/exchangerate?curry=" + curry,
                                    onclose: function () {
                                        if (this.returnValue) {
                                            inputid.val(this.returnValue.split("|")[0]);
                                            input.val(this.returnValue.split("|")[1]);
                                            curryy.val(this.returnValue.split("|")[2]);
                                            if (curryy == 1) {
                                                input.attr("disabled", true);
                                            }
                                        }
                                    }
                                }).show();
                            });
                        };
                    };

                }


                if (strprocessname == "Proc_TravelRequest") {
                    var internalorders = $(this).parent().parent().find($('input[name="item.internalorders"]')).val();
                    $("#internalorders").val(internalorders);
                    //差旅申请控制选择Others交通方式时，控制航班号必填或不必填
                    if ($("#vias").val() != "Others/其他") {
                        $("#flighttrainno").attr("class", "form-control detailinput needed");
                    }
                    else {
                        $("#flighttrainno").attr("class", "form-control detailinput");
                    }
                } else if (strprocessname == "Proc_CommonExpense") {
                    //普通费用的如果当前行是Taxi，那么显示 taxibtn  
                    var parExpensesid = $(this).parent().parent().find($('input[name="item.primaryexpensetypeid"]')).val();
                    $("#primaryexpensetype").val(parExpensesid);
                    var sectextid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
                    var sectext = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
                    var cnyamount = $(this).parent().parent().find($('input[name="item.cnyamount"]')).val();
                    var itemcurrencyid = $(this).parent().parent().find($('input[name="item.currencyid"]')).val();
                    var fxrate = $(this).parent().parent().find($('input[name="item.fxrate"]')).val();
                    var money = $.formatMoney(cnyamount, 2);
                    $("#cnyamount").val(cnyamount);
                    $("#lbcnyamout").html(money);
                    var interdorno = $(this).parent().parent().find($('input[name="item.internalorder"]')).val();
                    $("#internalorder").val(interdorno);
                    var praim = $(this).parent().parent().find($('input[name="item.primaryexpensetype"]')).val();

                    $.post("/FormCommonExpense/GetExpenseList", { parentcode: praim }, function (data, status) {
                        $("#secondaryexpensetype").html(data);
                        $("#secondaryexpensetype").children('option:selected').val(sectextid);
                        $("#secondaryexpensetype").children('option:selected').text(sectext);
                    });
                    $("#secondaryexpensetype").val(sectextid);
                    if (sectext.indexOf("Taxi") > -1 || sectext.indexOf("出租") > -1) {
                        $('#currency').attr("disabled", "true");
                        $('#primaryexpensetype').attr("disabled", true);
                        $('#secondaryexpensetype').attr("disabled", true);
                        $(".ExchangerateSearch").unbind("click");
                        $('#amount').attr("disabled", "true");
                        $('#fxrate').attr("disabled", "true");
                        getTaxi();
                    }
                    else {
                        $('#currency').attr("disabled", false);
                        $('#primaryexpensetype').attr("disabled", false);
                        $('#secondaryexpensetype').attr("disabled", false);
                        $('#amount').attr("disabled", false);
                        $('#fxrate').attr("disabled", false);
                        if (fxrate == 1) {
                            $("#fxrate").attr("disabled", true);
                        }
                        else { $("#fxrate").attr("disabled", false); }
                        $(".ExchangerateSearch").bind("click", function () {
                            var input = $(this).prev();
                            var inputid = $(this).next();
                            var curry = $("#currency").val();
                            var curryy = $("#currency");
                            dialog({
                                id: 'rate-dialog',
                                title: 'Exchangerate/汇率信息',
                                url: "/SelectPage/exchangerate?curry=" + curry,
                                onclose: function () {
                                    if (this.returnValue) {
                                        inputid.val(this.returnValue.split("|")[0]);
                                        input.val(this.returnValue.split("|")[1]);
                                        curryy.val(this.returnValue.split("|")[2]);
                                        if (curryy == 1) {
                                            input.attr("disabled", true);
                                        }
                                    }
                                }
                            }).show();
                        });
                    }
                } else if (strprocessname == "Proc_InvoicePaymentVerification") {
                    var ischeck = $(this).parent().parent().find($('input[name="item.closepo"]')).val();
                    var InvoiceNo = $(this).parent().parent().find($('input[name="item.invoiceno"]')).val();
                    var Invoiceid = $(this).parent().parent().find($('input[name="item.invoiceid"]')).val();
                    var filname = $(this).parent().parent().find($('input[name="item.filname"]')).val();
                    var poid = $(this).parent().parent().find($('input[name="item.poid"]')).val();

                    $("#invoiceno").val(InvoiceNo);
                    $("#invoiceid").val(Invoiceid);
                    $("#filname").val(filname);
                    if (ischeck == "true") {
                        $("#closepo").prop({ "checked": true });
                    } else {
                        $("#closepo").prop({ "checked": false });
                    }

                    var poidCol = $(".detailtable tbody tr.current").find("td").eq(8);
                    poidCol.append("<input type='hidden' value=" + poid + " name='item.poid'>");

                    AfterAction();

                } else if (strprocessname == "Proc_TravelRequest") {
                    var internalorders = $(this).parent().parent().find($('input[name="item.internalorders"]')).val();
                    $("#internalorders").val(internalorders);

                };
                $(".updatebtn").show();
                $(".addbtn").hide();
            });

            $(".detailtable .delbtn").bind("click", function () {

                if ($(this).parent().parent().next().find("td").eq(1).text().trim() == "Hotel Cost VAT") {
                    $(this).parent().parent().next().remove();
                }

                $(".detailinput").val("");
                $(".updatebtn").hide();
                $(".addbtn").show();
                $("#lbcnyamout").html("");
                var strprocessname = $("#processname").val();
                if (strprocessname == "Proc_TravelRequest") {
                    var costcenterid = $("#hiddencostcenterid").val();
                    $("#costcenter").val(costcenterid);
                }
                if (strprocessname == "Proc_TravelExpense") {
                    if ($(".detailtable tbody tr").length == 0) {
                        $("#total").html("");
                        $("#totalpayment").val("");
                        $("#sement").html("");
                        $("#finalreimbursement").val("");
                    }

                    var second = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
                    if (second == "Taxi Cost" || second == "出租车费") {
                        $("#TaxiJson").val("");
                    } else {
                        if (second == "Allowances" || second == "出差补贴") {
                            $("#AllowanceJson").val("");
                        }
                    }
                    $("#amount").attr("disabled", false);
                    $('#primaryexpensetype').attr("disabled", false);
                    $('#secondaryexpensetype').attr("disabled", false);
                    $('#currency').attr("disabled", false);
                    $('#fxrate').attr("disabled", false);

                    $(".ExchangerateSearch").bind("click", function () {
                        var input = $(this).prev();
                        var inputid = $(this).next();
                        var curry = $("#currency").val();
                        var curryy = $("#currency");
                        dialog({
                            id: 'rate-dialog',
                            title: 'Exchangerate/汇率信息',
                            url: "/SelectPage/exchangerate?curry=" + curry,
                            onclose: function () {
                                if (this.returnValue) {
                                    inputid.val(this.returnValue.split("|")[0]);
                                    input.val(this.returnValue.split("|")[1]);
                                    curryy.val(this.returnValue.split("|")[2]);
                                    if (curryy == 1) {
                                        input.attr("disabled", true);
                                    }
                                }
                            }
                        }).show();
                    });
                    var costcenterid = $("#hiddencostcenterid").val();
                    $("#tecostcenter").val(costcenterid);

                }
                else if (strprocessname == "Proc_CommonExpense") {
                    $("#amount").attr("disabled", false);
                    $("#primaryexpensetype").attr("disabled", false);
                    $('#secondaryexpensetype').attr("disabled", false);

                    $(".ExchangerateSearch").bind("click", function () {
                        var input = $(this).prev();
                        var inputid = $(this).next();
                        var curry = $("#currency").val();
                        var curryy = $("#currency");
                        dialog({
                            id: 'rate-dialog',
                            title: 'Exchangerate/汇率信息',
                            url: "/SelectPage/exchangerate?curry=" + curry,
                            onclose: function () {
                                if (this.returnValue) {
                                    inputid.val(this.returnValue.split("|")[0]);
                                    input.val(this.returnValue.split("|")[1]);
                                    curryy.val(this.returnValue.split("|")[2]);
                                    if (curryy == 1) {
                                        input.attr("disabled", true);
                                    }
                                }
                            }
                        }).show();
                    });
                    var second = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
                    if (second.indexOf("Taxi Cost") > -1 || second.indexOf("出租车费") > -1) {
                        $("#TaxiJson").val("");
                    };
                    var costcenterid = $("#hiddencostcenterid").val();
                    $("#tecostcenter").val(costcenterid);
                }
                else {
                    if (strprocessname == "Proc_InvoicePaymentVerification") {
                        $(this).parent().parent().remove();

                        AfterAction();

                        if ($(".detailtable tbody tr").length == 0) {
                            $("#totalpayment").val("");
                            $("#total").html("");
                            $("#finalpaymentamount").val("");
                            $("#sement").html("");
                            $("#inwords").html("");
                            $("#amountinwords").val("");
                            $("#thecombined").val("");
                            $("#cnytotal").html("");
                        }
                    }
                }
                $(this).parent().parent().remove();

                if (strprocessname == "Proc_TravelExpense" || strprocessname == "Proc_CommonExpense") {
                    var costcenterid = $("#hiddencostcenterid").val();
                    $("#costcenter").val(costcenterid);
                    var semennt = $("#totalpayment").val() - $("#deductedadvances").val();
                    if (semennt < 0) {
                        semennt = 0;
                    }
                    $("#sement").html(semennt);
                    $("#finalreimbursement").val(semennt);
                }

                if (thisbtn.hasClass("afterfun")) {
                    AfterAction();
                }
            });

        }
        $(".needed").removeClass("required");

        if ($(this).hasClass("afterfun")) {
            AfterAction();
        }
        //当必填项未填时去判断是否选中
        if ($("#expensetype").val() == "" || $("#currency").val() == "" || $("#amount").val() == "" || $("#invoiceno").text() == "") {
            if ($("#closepo").is(":checked")) {
                $("#closepo").attr("checked", true);
                $("#closepo").val("true");
                $("#po").val("true");
            }
        } else {
            $("#closepo").prop("checked", false);
        }
    });

    //明细修改
    $(".updatebtn").click(function () {
        var strprocessname = $("#processname").val();
        //2015-10-15 Modify By Sherry 同意修改时也要验证不能为空
        $(".needed").addClass("required");
        if ($(this).hasClass("before")) {
            BeforeAction();
        }
        var checkvalidator = true;
        var validator = $(".Form").validate();
        for (i = 0; i < $(".detailinput").length; i++) {
            if (!validator.element($(".detailinput").eq(i))) {
                checkvalidator = false;
            };
        } if (checkvalidator) {
            var currenttr = $(".detailtable tbody tr.current");
            for (i = 0; i < currenttr.find("td").length - 1; i++) {
                var detailinput = $(".detailinput").eq(i);
                if (detailinput.is("select"))
                    currenttr.find("td").eq(i).html(detailinput.children('option:selected').text() + "<input type='hidden' name='item." + (detailinput.attr("name") == "traveltypesel" ? "traveltype" : detailinput.attr("name")) + "' value=\"" + detailinput.children('option:selected').text() + "\"><input type='hidden' name='item." + (detailinput.attr("name") == "travelrequestno" ? "travelrequest" : (detailinput.attr("name") == "traveltypesel" ? "traveltype" : detailinput.attr("name"))) + "id' value=\"" + detailinput.val() + "\">");
                else
                    currenttr.find("td").eq(i).html(detailinput.val() + "<input type='hidden' name='item." + detailinput.attr("name") + "' value=\"" + detailinput.val() + "\">");
            }
            if ($(this).hasClass("afterfun")) {
                $("#explanation").removeClass("required");
                AfterAction();
            }

            if (strprocessname == "Proc_InvoicePaymentVerification") {
                var invoice = $("#invoiceid").val();
                var fil = $("#filname").val();
                var invos = $(".detailtable tbody tr.current").find("td").eq(6);
                var invoiceno = invos.find("input").eq(0).val();
                var expensetypeid = $("#expensetypeid").val();
                var poid = $("#poid").val();
                invos.html("");
                invos.append((fil != "" ? "<a href='" + "/Uploads" + fil + "'target='_blank'>" + invoiceno + "</a>" : invoiceno) + "<input type='hidden' value='" + fil + "' name='item.filname'>" + "<input type='hidden' value='" + invoiceno + "' name='item.invoiceno'>" + "</a><input type='hidden' value='" + invoice + "' name='item.invoiceid'>" + "");
                var Expensetype = $(".detailtable tbody tr.current").find("td").eq(1);
                Expensetype.append("<input type='hidden' value='" + expensetypeid + "' name='item.expensetypeid'>");
                var poidCol = $(".detailtable tbody tr.current").find("td").eq(8);
                poidCol.append("<input type='hidden' value='" + poid + "' name='item.poid'>");
                var rate = $("#rate").val();
                $(".detailtable tbody tr.current").find("td").eq(2).find("input").eq(2).val(rate);
                AfterAction();
            }

            if (strprocessname == "Proc_TravelExpense") {
                var travelid = $("#istraveno").val();
                $(".detailtable tbody tr.current").find("td").eq(9).find("input").eq(1).val(travelid);
                $("#travel").hide();
                var costelement = $("#costelement").val();
                if (($("#secondaryexpensetype").children("option:selected").text().trim().indexOf('Hotel Cost') > -1 ||
                    $("#secondaryexpensetype").children("option:selected").text().trim().indexOf('酒店') > -1) && $("#isvat").val() == "1") {
                    var secondaryexpensetype = $("#secondaryexpensetype").val();
                    var vatclonre = $(".detailtable tbody tr.current").clone(true);
                    //vatclonre.find("td").eq(3).find("input").val("261200");
                    var isvat = $("#isvat").val();
                    var vatvalue = $("#vat").val();
                    vatclonre.find("td").eq(1).text("Hotel Cost VAT");
                    vatclonre.find("td").eq(1).append("<input type='hidden' value='Hotel Cost VAT' name='item.secondaryexpensetype'>");
                    vatclonre.find("td").eq(1).append("<input type='hidden' value='" + secondaryexpensetype + "' name='item.secondaryexpensetypeid'>");
                    vatclonre.find("td").eq(3).html("");
                    vatclonre.find("td").eq(3).text("261200");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + 261200 + "' name='item.costelement'>");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + isvat + "' name='item.isvat'>");
                    vatclonre.find("td").eq(3).append("<input type='hidden' value='" + vatvalue + "' name='item.tax'>");
                    vatclonre.find("td").eq(12).text("");
                    var vatamount = $("#amount").val();
                    var rate = vatclonre.find("td").eq(2).find("input").eq(1).val();
                    if (vatvalue != "") {
                        var vatamuont = vatamount * vatvalue;
                    }
                    else { var vatamuont = vatamount * rate; }

                    var vatatotal = $.formatMoney(vatamuont, 2);
                    vatclonre.find("td").eq(4).text(vatatotal);
                    vatclonre.find("td").eq(4).append("<input type='hidden' value='" + vatamuont + "' name='item.amount'>");
                    var currencyid = vatclonre.find("td").eq(2).find("input").eq(1).val();
                    if (currencyid == $("#BaseCurrencyID").val()) {
                        var LocalCurrency = (1 * vatamuont).toFixed(2);
                        vatclonre.find("td").eq(8).text(LocalCurrency);
                        vatclonre.find("td").eq(8).append("<input type='hidden' value='" + LocalCurrency + "' name='item.cnyamount'>");
                        AfterAction();
                    }
                    else {
                        $.post("/Base/getcurrency", {
                            currencyid: currencyid,
                        }, function (data, status) {
                            var curr = data;
                            var LocalCurrency = (curr * vatamuont).toFixed(2);
                            vatclonre.find("td").eq(8).text(LocalCurrency);
                            vatclonre.find("td").eq(8).append("<input type='hidden' value='" + LocalCurrency + "' name='item.cnyamount'>");
                            AfterAction();
                        });
                    }

                    vatclonre.find("td").eq(11).text("");
                    vatclonre.find("td").eq(11).append("<input type='hidden' name='item.explanation' value='' />")
                    $(".detailtable tbody tr.current").after(vatclonre);
                    $(".isvat").hide();
                    $(".vat").hide();
                    $("#isvat").attr("checked", false);
                }

                $(".detailtable tbody tr").each(function () {
                    //   var td = $(this).find("td").eq(11);
                    var addinput = $(this).find("td").eq(3);
                    var trvitd = $(this).find("td").eq(9);

                    if (addinput.find("input[name='item.isvat']").length <= 0) {
                        addinput.append("<input type='hidden' value='" + vatvalue + "' name='item.tax'class='tax'>");
                        addinput.append("<input type='hidden' value='" + "" + "' name='item.isvat' class='isvat'>");
                        //trvitd.append("<input type='hidden' value='" + "" + "' name='item.travelrequestid'>");
                    }

                    var typename = $(this).find("td").eq(0).find("input").eq(0).val();
                    //添加空的隐藏域   填充空值

                    if (trvitd.find("input[name='item.travelrequestno']").length <= 0) {

                        if (trvitd.find("input[name='item.istraveno']").length > 0) {
                            trvitd.text("");
                        }

                        trvitd.append("<input type='hidden' value='" + "" + "' name='item.travelrequestid' class='istravelrequestid'>");
                        trvitd.append("<input type='hidden' value='" + "" + "' name='item.travelrequestno'>");
                    }

                });

                AfterAction();
                $("#isvat").val(0);
            }
            $("#lbcnyamout").html("");
            $(".detailtable tbody tr").each(function () {
                if ($(this).prop("checked") == true) {
                    $(this).attr("checked", true);
                }
                else {
                    $(this).attr("checked", false);
                }
            })
            currenttr.removeClass("current");
            $(".detailinput").val("");

            //if (strprocessname == "Proc_AdvancePayment" || strprocessname == "Proc_InvoicePaymentVerification") {
            //设置币种和汇率默认值
            $("#currency").val($("#baseCurrencyId").val());
            $("#fxrate").val(1);

            var hiddencostcenterid = $("#hiddencostcenterid").val()
            $("#costcenter").val(hiddencostcenterid);
            $("#tecostcenter").val(hiddencostcenterid);
            $(".needed").removeClass("required");
            $(".updatebtn").hide();
            $(".addbtn").show();
            if (strprocessname == "Proc_CommonExpense") {
                $("#amount").attr("disabled", false);
                $('#primaryexpensetype').attr("disabled", false);
                $('#secondaryexpensetype').attr("disabled", false);
            }
            else {
                if (strprocessname == "Proc_TravelExpense") {
                    $(".isvat").hide();
                    $(".vat").hide();
                    $("#isvat").attr("checked", false);
                    $(".TaxiDetails").hide();
                    $("#fxrate").attr("disabled", false);
                    $("#currency").attr("disabled", false);
                    $("#amount").attr("disabled", false);
                    $('#primaryexpensetype').attr("disabled", false);
                    $('#secondaryexpensetype').attr("disabled", false);
                    $(".ExchangerateSearch").bind("click", function () {
                        var input = $(this).prev();
                        var inputid = $(this).next();
                        var curry = $("#currency").val();
                        var curryy = $("#currency");
                        dialog({
                            id: 'rate-dialog',
                            title: 'Exchangerate/汇率信息',
                            url: "/SelectPage/exchangerate?curry=" + curry,
                            onclose: function () {
                                if (this.returnValue) {
                                    inputid.val(this.returnValue.split("|")[0]);
                                    input.val(this.returnValue.split("|")[1]);
                                    curryy.val(this.returnValue.split("|")[2]);
                                    if (curryy == 1) {
                                        input.attr("disabled", true);
                                    }
                                }
                            }
                        }).show();
                    });

                }
            }
        };


        if (strprocessname == "Proc_InvoicePaymentVerification") {
            $("#closepo").attr("checked", false);

        }
        //修改明细 后 再一次增加下去，小类显示“Please Select / 请选择”
        if ($("#secondaryexpensetype").val() == null) {
            var val = $("#secondaryexpensetype").find("option").eq(0).val("");
            var text = $("#secondaryexpensetype").find("option").eq(0).text("Please Select / 请选择");
            $("#secondaryexpensetype").val("");
        }
    });

    $(".detailtable .editbtn").click(function () {
        $(".detailinput").val("");
        var tr = $(this).parent().parent();
        tr.parent().find("tr").removeClass("current");
        tr.addClass("current");
        for (i = 0; i < tr.find("td").length - 1; i++) {
            var td = tr.find("td").eq(i);
            var detailinput = $(".detailinput").eq(i);
            if (detailinput.is("select")) {
                for (var j = 0; j < detailinput.get(0).options.length ; j++) {
                    if (detailinput.get(0).options[j].text == td.find("input").eq(0).val() || detailinput.get(0).options[j].text == td.find("input").eq(1).val()) {
                        detailinput.get(0).options[j].selected = true;
                        break;
                    }
                }
            }
            else {
                if (td.find("input").length > 1) {
                    detailinput.val(td.find("input").eq(1).val());
                }
                else {
                    detailinput.val(td.find("input").eq(0).val());
                }
            }
        }

        var strprocessname = $("#processname").val();
        if (strprocessname == "Proc_TravelRequest") {
            var internalorders = $(this).parent().parent().find($('input[name="item.internalorders"]')).val();
            $("#internalorders").val(internalorders);
            //差旅申请控制选择Others交通方式时，控制航班号必填或不必填
            if ($("#vias").val() != "Others/其他") {
                $("#flighttrainno").attr("class", "form-control detailinput needed");
            }
            else {
                $("#flighttrainno").attr("class", "form-control detailinput");
            }
        } else if ($("#CommonExpense").val() == "Proc_CommonExpense") {
            var parExpensesid = $(this).parent().parent().find($('input[name="item.primaryexpensetypeid"]')).val();
            $("#primaryexpensetype").val(parExpensesid);
            var currencyid = $(this).parent().parent().find($('input[name="item.currencyid"]')).val();
            var fxrate = $(this).parent().parent().find($('input[name="item.fxrate"]')).val();
            //$("#currency").val(currencyid);
            var cnyamount = $(this).parent().parent().find($('input[name="item.cnyamount"]')).val();
            var money = $.formatMoney(cnyamount, 2);
            $("#cnyamount").val(cnyamount);
            $("#lbcnyamout").html(money);
            //var edibtn = $(".detailtable .editbtn");
            var sec = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
            var sectext = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
            var praim = $(this).parent().parent().find($('input[name="item.primaryexpensetype"]')).val();
            $.post("/FormCommonExpense/GetExpenseList", { parentcode: praim }, function (data, status) {
                $("#secondaryexpensetype").html(data);
                $("#secondaryexpensetype").children('option:selected').val(sec);
                $("#secondaryexpensetype").children('option:selected').text(sectext);
            });
            $("#secondaryexpensetype").val(sec);
            //普通费用的如果当前行是Taxi，那么显示 taxibtn                     
            if (sectext.indexOf("Taxi") > -1 || sectext.indexOf("出租车") > -1) {
                $('#currency').attr("disabled", "true");
                $('#primaryexpensetype').attr("disabled", true);
                $('#secondaryexpensetype').attr("disabled", true);
                $(".ExchangerateSearch").unbind("click");
                $('#amount').attr("disabled", "true");
                $('#fxrate').attr("disabled", "true");
                getTaxi();
                $(".Taxibtn").show()
            }
            else {
                $(".Taxibtn").hide()
                $('#currency').attr("disabled", false);
                $('#primaryexpensetype').attr("disabled", false);
                $('#secondaryexpensetype').attr("disabled", false);
                $('#amount').attr("disabled", false);
                $('#fxrate').attr("disabled", false);
                if (fxrate == 1) {
                    $("#fxrate").attr("disabled", true);
                }
                else { $("#fxrate").attr("disabled", false); }
                $(".ExchangerateSearch").bind("click", function () {
                    var input = $(this).prev();
                    var inputid = $(this).next();
                    var curry = $("#currency").val();
                    var curryy = $("#currency");
                    dialog({
                        id: 'rate-dialog',
                        title: 'Exchangerate/汇率信息',
                        url: "/SelectPage/exchangerate?curry=" + curry,
                        onclose: function () {
                            if (this.returnValue) {
                                inputid.val(this.returnValue.split("|")[0]);
                                input.val(this.returnValue.split("|")[1]);
                                if (this.returnValue.split("|")[2] != null) {
                                    curryy.val(this.returnValue.split("|")[2]);
                                }

                                if (curryy == 1) {
                                    input.attr("disabled", true);
                                }
                            }
                        }
                    }).show();
                });
            }
        } else if (strprocessname == "Proc_TravelExpense") {

            //差旅费用的如果当前行是Taxi，那么显示 taxibtn 
            var parExpensesid = $(this).parent().parent().find($('input[name="item.primaryexpensetypeid"]')).val();
            $("#primaryexpensetype").val(parExpensesid);
            var sectext = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
            var sectextid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();
            var cny = $(this).parent().parent().find($('input[name="item.cnyamount"]')).val();
            var cost = $(this).parent().parent().find($('input[name="item.costelement"]')).val();
            var secondaryexpensetypeid = $(this).parent().parent().find($('input[name="item.secondaryexpensetypeid"]')).val();

            var itemtravelrequest = $(this).parent().parent().find($('input[name="item.travelrequest"]')).val();
            var itemtravelrequestid = $(this).parent().parent().find($('input[name="item.travelrequestid"]')).val();
            var itemcurrencyid = $(this).parent().parent().find($('input[name="item.currencyid"]')).val();

            var fxrate = $(this).parent().parent().find($('input[name="item.fxrate"]')).val();
            var tax = $(this).parent().parent().find($('input[name="item.tax"]')).val();
            $("#fxrate").val(fxrate);
            var isvat = $(this).parent().parent().next().find($('input[name="item.isvat"]')).val();
            if (isvat == "1") {
                $(".isvat").show();
                $("#isvat").prop("checked", true);
                $("#isvat").val(1);
                $("#vat").show();
                $("#vat").val(tax);
            }
            else {
                $("#isvat").prop("checked", false)
                $("#isvat").hide();
            }

            if ((sectext.indexOf("Hotel Cost") > -1 || sectext.indexOf("酒店") > -1) && isvat == "1") {
                $(this).parent().parent().next().remove();
            }


            if (itemcurrencyid == 1) {
                $("#fxrate").attr("disabled", "true");
            }
            else { $("#fxrate").attr("disabled", false); }
            $("#istraveno").val(itemtravelrequestid);
            if (sectext.indexOf("Hotel Cost") > -1 || sectext.indexOf("酒店") > -1) {
                $("#travel").addClass("detailinput");
                $("#istraveno").removeClass("detailinput");
                var tramount = $("#tramount").val();
                var cha = cny - tramount;
                if (cha > 0) {
                    $("#explanation").addClass("required");
                }
                else { $("#explanation").removeClass("required"); }
                var traveldd = $("#travel").find("option").eq(0).val();
                //var traveldd= $("#travel").find("option:selected").val();
                if (traveldd == "" || traveldd == null) {
                    $(".detailtabletravel tbody tr").each(function () {
                        var itemtravelrequestid = $(this).find("td").eq(0).find("input").eq(0).val();
                        var itemtraveltaxt = $(this).find("td").eq(0).text();
                        $("#travel").append("<option value=\"" + itemtravelrequestid + "\">" + itemtraveltaxt + "</option>");

                    });
                    $("#travel").show();
                }
                else {

                    $("#travel").children("option").each(function () {
                        var temp_value = $(this).val();
                        if (temp_value == itemtravelrequestid) {
                            $(this).attr("selected", true);
                        }
                    });

                    $("#travel").show();
                }
            }

            else { $("#travel").hide(); }
            $("#costelement").val(cost);
            $("#lbcnyamout").html(cny);
            $("#secondaryexpensetype").val(sectextid);

            var praim = $(this).parent().parent().find($('input[name="item.primaryexpensetype"]')).val();
            $.post("/FormTravelExpense/GetExpenseList", { parentcode: praim }, function (data, status) {
                $("#secondaryexpensetype").html(data);
                $("#secondaryexpensetype").children('option:selected').val(sectextid);
                $("#secondaryexpensetype").children('option:selected').text(sectext);
            });
            if (sectext.indexOf("Taxi") > -1 || sectext.indexOf("出租") > -1) {
                $('#currency').attr("disabled", "true");
                $('#primaryexpensetype').attr("disabled", true);
                $('#secondaryexpensetype').attr("disabled", true);
                $(".ExchangerateSearch").unbind("click");
                $('#amount').attr("disabled", "true");
                $('#fxrate').attr("disabled", "true");
                gettaxi();
                $(".TaxiDetails").show();
                //弹框出租车页面
            }
            else {
                $(".TaxiDetails").hide();
                if (sectext.indexOf("Allowances") > -1 || sectext.indexOf("出差补贴") > -1) {
                    $('#currency').attr("disabled", "true");
                    $('#primaryexpensetype').attr("disabled", true);
                    $('#secondaryexpensetype').attr("disabled", true);
                    $(".ExchangerateSearch").unbind("click");
                    $('#amount').attr("disabled", "true");
                    $('#fxrate').attr("disabled", "true");

                    var costelement = $(this).parent().parent().find($('input[name="item.costelement"]')).val();
                    $("#costelement").val(costelement);

                    getallowance();
                    //弹框出津贴页面
                    $(".AllowanceDetails").show();
                }
                else {
                    $(".AllowanceDetails").hide()
                    $('#currency').attr("disabled", false);
                    $('#primaryexpensetype').attr("disabled", false);
                    $('#secondaryexpensetype').attr("disabled", false);
                    $('#amount').attr("disabled", false);
                    $('#fxrate').attr("disabled", false);
                    if (itemcurrencyid == 1) {
                        $("#fxrate").attr("disabled", true);
                    }
                    else { $("#fxrate").attr("disabled", false); }
                    $(".ExchangerateSearch").bind("click", function () {
                        var input = $(this).prev();
                        var inputid = $(this).next();
                        var curry = $("#currency").val();
                        var curryy = $("#currency");
                        dialog({
                            id: 'rate-dialog',
                            title: 'Exchangerate/汇率信息',
                            url: "/SelectPage/exchangerate?curry=" + curry,
                            onclose: function () {
                                if (this.returnValue) {
                                    inputid.val(this.returnValue.split("|")[0]);
                                    input.val(this.returnValue.split("|")[1]);
                                    curryy.val(this.returnValue.split("|")[2]);
                                    if (curryy == 1) {
                                        input.attr("disabled", true);
                                    }
                                }
                            }
                        }).show();
                    });
                };
            };
        }
        else if (strprocessname == "Proc_InvoicePaymentVerification") {
            var InvoiceNo = $(this).parent().parent().find($('input[name="item.invoiceno"]')).val();
            $("#invoiceno").val(InvoiceNo);
            var ischeck = $(this).parent().parent().find($('input[name="item.closepo"]')).val();
            if (ischeck == "true") {
                $("#closepo").prop({ "checked": true });
            } else {
                $("#closepo").prop({ "checked": false });
            }

            var expenseTypeid = $(this).parent().parent().find($('input[name="item.expensetypeid"]')).val();
            $("#expensetypeid").val(expenseTypeid);
            $("#expensetype").val($(this).parent().parent().find($('input[name="item.expensetype"]')).val());
            var poid = $(this).parent().parent().find($('input[name="item.poid"]')).val();
            $("#poid").val(poid);
            $("#pono").val($(this).parent().parent().find($('input[name="item.pono"]')).val());

            var invoiceNo = $(this).parent().parent().find($('input[name="item.invoiceno"]')).val();

            //获得发票信息 用以验证 手动输入的信息和发票信息是否相符
            $.post("/FormInvoicePaymentVerification/getinvo", {
                InvoiceNo: invoiceNo
            }, function (data, status) {
                if (data != "") {
                    $("#invoiceid").val(data.id);
                    $("#filname").val(data.file_name);
                    $("#invCurrency").val(data.currency);
                    $("#invTotal").val(data.total);
                    $("#invDate").val(data.inv_date);
                }
            });
            // AfterAction();
            $("#rate").val($(this).parent().parent().find("td").eq(2).find("input[name='item.fxrate']").val());
        };

        $(".updatebtn").show();
        $(".addbtn").hide();
    });

    $(".detailtable .delbtn").click(function () {

        if ($(this).parent().parent().next().find("td").eq(1).text().trim() == "Hotel Cost VAT") {
            $(this).parent().parent().next().remove();
        }

        $(".detailinput").val("");

        var strprocessname = $("#processname").val();
        if (strprocessname == "Proc_TravelRequest") {
            var costcenterid = $("#hiddencostcenterid").val();
            $("#costcenter").val(costcenterid);
        }
        if (strprocessname == "Proc_TravelExpense") {
            var second = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();
            if (second == "Taxi Cost" || second == "出租车费") {
                $("#TaxiJson").val("");
            } else {
                if (second == "Allowances" || second == "出差补贴") {
                    $("#AllowanceJson").val("");
                }
            }
            $("#amount").attr("disabled", false);
            $('#primaryexpensetype').attr("disabled", false);
            $('#secondaryexpensetype').attr("disabled", false);
            $('#currency').attr("disabled", false);
            $('#fxrate').attr("disabled", false);

            $(".ExchangerateSearch").bind("click", function () {
                var input = $(this).prev();
                var inputid = $(this).next();
                var curry = $("#currency").val();
                var curryy = $("#currency");
                dialog({
                    id: 'rate-dialog',
                    title: 'Exchangerate/汇率信息',
                    url: "/SelectPage/exchangerate?curry=" + curry,
                    onclose: function () {
                        if (this.returnValue) {
                            inputid.val(this.returnValue.split("|")[0]);
                            input.val(this.returnValue.split("|")[1]);
                            if (this.returnValue.split("|")[2] != null) {
                                curryy.val(this.returnValue.split("|")[2]);
                            }


                            if (curryy == 1) {
                                input.attr("disabled", true);
                            }
                        }
                    }
                }).show();
            });

        }
        else if (strprocessname == "Proc_CommonExpense") {
            var second = $(this).parent().parent().find($('input[name="item.secondaryexpensetype"]')).val();

            if (second.indexOf("Taxi Cost") > -1 || second.indexOf("出租车费") > -1) {
                $("#TaxiJson").val("");
            }

            $("#amount").attr("disabled", false);
            $('#primaryexpensetype').attr("disabled", false);
            $('#secondaryexpensetype').attr("disabled", false);
            $(".ExchangerateSearch").bind("click", function () {
                var input = $(this).prev();
                var inputid = $(this).next();
                var curry = $("#currency").val();
                var curryy = $("#currency");
                dialog({
                    id: 'rate-dialog',
                    title: 'Exchangerate/汇率信息',
                    url: "/SelectPage/exchangerate?curry=" + curry,
                    onclose: function () {
                        if (this.returnValue) {
                            inputid.val(this.returnValue.split("|")[0]);
                            input.val(this.returnValue.split("|")[1]);
                            if (this.returnValue.split("|")[2] != null) {
                                curryy.val(this.returnValue.split("|")[2]);
                            }

                            if (curryy == 1) {
                                input.attr("disabled", true);
                            }
                        }
                    }
                }).show();
            });
        }
        else {
            if (strprocessname == "Proc_InvoicePaymentVerification") {
                $(this).parent().parent().remove();
                AfterAction();
                if ($(".detailtable tbody tr").length == 0) {
                    $("#totalpayment").val("");
                    $("#total").html("");
                    $("#finalpaymentamount").val("");
                    $("#sement").html("");
                    $("#inwords").html("");
                    $("#amountinwords").val("");
                    $("#thecombined").val("");
                    $("#cnytotal").html("");
                }
            }
        }

        $(this).parent().parent().remove();

        if (strprocessname == "Proc_TravelExpense" || strprocessname == "Proc_CommonExpense") {
            var costrter = $("#hiddencostcenterid").val();
            $("#tecostcenter").val(costrter);
        }
        $("#lbcnyamout").html("");
        $(".updatebtn").hide();
        $(".addbtn").show();
        if ($(this).hasClass("afterfun")) {
            AfterAction();
        }

        if (strprocessname == "Proc_TravelExpense" || strprocessname == "Proc_CommonExpense") {
            var semennt = $("#totalpayment").val() - $("#deductedadvances").val();
            if (semennt < 0) {
                semennt = 0;
            }
            var money = $.formatMoney(sement, 2);
            $("#sement").html(money);
            $("#finalreimbursement").val(semennt);
        }
        if (strprocessname == "Proc_InvoiceIssuance") {
            $(this).parent().parent().remove();
            if ($(".detailtable tbody tr").length == 0) {
                $("#totalpayment").val(0);
                $("#total").html(0);
                var amountinwords = convertCurrency($("#totalpayment").val());
                $("#inwords").html(amountinwords);
                $("#amountinwords").val(amountinwords);
            }
            AfterAction();
        }
        if ($(".detailtable tbody tr").length == 0) {
            $("#totalpayment").val(""); //将总费用显示到对应文本框对象中
            $("#total").html("");
            $("#thecombined").val("");
            $("#cnytotal").html("");
            var amountinwords = convertCurrency($("#totalpayment").val());
            $("#inwords").html(amountinwords);
            $("#amountinwords").val(amountinwords);
        }
    });

    $(".CostCenterMultiSearch").click(function () {

        var input = $(this).prev();
        var inputID = $(this).next();
        dialog({
            id: 'costcenter-dialog',
            title: 'CostCenter 成本中心',
            url: "/SelectPage/CostcenterMultiSelect",
            onclose: function () {
                if (this.returnValue) {
                    inputID.val(this.returnValue.split('|')[0]);
                    input.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });

    $(".CostCenterSearch").click(function () {
        var pettycashcc = $("#pettycashcc");
        var input = $("#costcenter");
        var eccr = $("#ercc");
        var tecostcenter = $("#tecostcenter");
        var center = $(this).prev();
        var inputID = $(this).next();//2015-12-23 modify sherry
        dialog({
            id: 'costcenter-dialog',
            title: 'CostCenter 成本中心',
            url: "/SelectPage/Costcenter",
            onclose: function () {
                if (this.returnValue) {
                    inputID.val(this.returnValue.split('|')[0]);//2015-10-15 Modify by Sherry 添加回传的成本中心的ID值
                    input.val(this.returnValue.split('|')[1]);
                    center.val(this.returnValue.split('|')[1]);
                    eccr.val(this.returnValue.split('|')[1]);
                    pettycashcc.val(this.returnValue.split('|')[1]);
                    tecostcenter.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });

    $(".CostCenterIDSearch").click(function () {
        var input = $("#costcenter");
        var center = $(this).prev();
        dialog({
            id: 'costcenter-dialog',
            title: 'CostCenter 成本中心',
            url: "/SelectPage/Costcenter",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                    center.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });
    $(".AssetNoSearch").click(function () {

        var input = $("#assetsno");
        var coster = $("#coser").val();
        dialog({
            id: 'costcenter-dialog',
            title: 'Assets 固定资产',
            url: "/SelectPage/Getassts?cosrter=" + coster,
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[2]);

                }
            }
        }).show();
    });
    $(".PayeeeSearch").click(function () {
        var inputPayee = $("#payee");
        var inputPayeeId = $("#payeeid");
        var inputPayeeGlAccount = $("#payeeglaccount");
        var inputPayeeBankAccount = $("#bankac");
        var inputPayeeBankName = $("#bankname");
        var bankkey = $("#bankkey");
        dialog({
            id: 'InternalBank-dialog',
            title: 'Internal Bank Account 内部银行账号信息',
            url: "/SelectPage/GetInternetBankAccount",
            onclose: function () {
                if (this.returnValue) {
                    inputPayee.val(this.returnValue.split('|')[0]);
                    inputPayeeBankName.val(this.returnValue.split('|')[1]);
                    inputPayeeBankAccount.val(this.returnValue.split('|')[2]);
                    inputPayeeGlAccount.val(this.returnValue.split('|')[3]);
                    inputPayeeId.val(this.returnValue.split('|')[4]);
                    bankkey.val(this.returnValue.split('|')[5]);
                }
            }

        }).show();
    });
    $(".DraweeSearch").click(function () {
        var inputDrawee = $("#drawee");
        var inputDraweeId = $("#draweeid");
        var inputDraweeBankName = $("#draweebankname");
        var inputDraweeBankAccount = $("#draweebankac");
        var inputDraweeGlaccount = $("#glaccount");
        dialog({
            id: 'InternalBank-dialog',
            title: 'Internal Bank Account 内部银行账号信息',
            url: "/SelectPage/GetInternetBankAccount",
            onclose: function () {
                if (this.returnValue) {
                    inputDrawee.val(this.returnValue.split('|')[0]);
                    inputDraweeBankName.val(this.returnValue.split('|')[1]);
                    inputDraweeBankAccount.val(this.returnValue.split('|')[2]);
                    inputDraweeGlaccount.val(this.returnValue.split('|')[3]);
                    inputDraweeId.val(this.returnValue.split('|')[4]);
                }
            }

        }).show();
    });
    $(".VendorCodeSearch").click(function () {
        var input = $(this).prev();
        var inputname = $("#vendorname");
        var inputno = $("#bankac");
        var inputbankname = $("#bankname");
        var currency = $("#currency");
        dialog({
            id: 'vendor-dialog',
            title: 'Vendor 供应商',
            url: "/SelectPage/newVendor",
            onclose: function () {
                if (this.returnValue) {

                    input.val(this.returnValue.split('|')[0]);
                    inputname.val(this.returnValue.split('|')[1]);
                    inputno.val(this.returnValue.split('|')[3]);
                    inputbankname.val(this.returnValue.split('|')[2]);
                    currency.val(this.returnValue.split('|')[5]);
                    $.post("/FormAdvancePayment/getcurrency", {
                        currencyid: this.returnValue.split('|')[5]
                    }, function (data, status) {
                        var curr = data;
                        if (curr == "") {
                            $("#fxrate").val(1);
                        } else {
                            $("#fxrate").val(curr)
                        }
                        //  $("#currency").attr("disabled", true);
                    });

                    if ($(".VendorCodeSearch").hasClass("Invoicefun")) {
                        invoicefun();
                    }
                }
            }
        }).show();
    });



    $(".IntOrderNoSearch").click(function () {
        var input = $(this).prev();
        var inputid = $("#internalorderid");
        var nextid = $(this).parent().find("#shareinternalorderid");
        dialog({
            id: 'IntOrderNo-dialog',
            title: 'IntOrderNo 项目编号',
            url: "/SelectPage/Internalorder",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                    inputid.val(this.returnValue.split('|')[1]);
                    nextid.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });
    $(".CostelementSearch").click(function () {
        var input = $(this).prev();
        dialog({
            id: 'Costelement-dialog',
            title: 'Costelement 会计科目编号',
            url: "/SelectPage/CostElement",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue);
                }
            }
        }).show();
    });
    $(".PONOSearch").click(function () {
        var vendorcode = $("#vendorcode").val();
        var input = $(this).prev();
        var inputVal = $(this).next();
        dialog({
            id: 'PONO-dialog',
            title: 'PO No. 订单编号',
            url: "/SelectPage/PoNo?vendorcode=" + vendorcode,
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                    inputVal.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });

    $(".DealerCodeSearch").click(function () {
        var input = $(this).prev();
        var inputDealerName = $("#dealername");
        var inputbankName = $("#bankname");
        var inputBankAC = $("#bankac");
        dialog({
            id: 'DealerCode-dialog',
            title: 'DealerCode 经销商编号',
            url: "/SelectPage/Customer",
            onclose: function () {
                if (this.returnValue) {
                    $(".detailtable tbody").html("");
                    //选择经销商后  置空
                    $("#total").html("0");
                    $("#totalpayment").val(0);
                    $("#inwords").html("零");
                    $("#amountinwords").val("零");

                    input.val(this.returnValue.split('|')[0]);
                    inputDealerName.val(this.returnValue.split('|')[1]);
                    $.post("/FormSalesIncentivePayout/getnamevode", {
                        name: inputDealerName.val()
                    }, function (data, status) {
                        inputbankName.val(data.bankname);
                        inputBankAC.val(data.bankaccount);
                    });

                }
            }
        }).show();
    });

    $(".chekdealercode").change(function () {
        if ($(this).val() != "") {
            $.post("/SelectPage/GetDealerCode", {
                costdealer: $(this).val()
            }, function (data, status) {
                if (data == "False") {
                    updateAlert("Fill in the wrong dealer code./经销商编号填写错误。");
                    $(".chekdealercode").val("");
                    return false;
                } else {
                    return true;
                };
            });
        }
    });
    $(".ORGDeptmentSearch").click(function () {
        var input = $(this).prev();
        var nextinput = $(this).next();
        var transferredindeptid = $("#transferredindeptid");
        var orgname = $("#organization").val();
        dialog({
            id: 'Department-dialog',
            title: 'Department 部门',
            url: "/SelectPage/ORGDepartment/?organization=" + orgname,
            onclose: function () {
                if (this.returnValue) {
                    nextinput.val(this.returnValue.split('|')[0]);
                    input.val(this.returnValue.split('|')[1]);
                    transferredindeptid.val(this.returnValue.split('|')[0]);
                }
            }
        }).show();
    });
    $(".ORGUserSearch").click(function () {
        var input = $(this).prev();
        var nextinput = $(this).next();
        var ownerid = $("#ownerid");
        dialog({
            id: 'User-dialog',
            title: 'User 用户',
            url: "/SelectPage/ORGUser",
            onclose: function () {
                if (this.returnValue) {
                    nextinput.val(this.returnValue.split('|')[0]);
                    input.val(this.returnValue.split('|')[1]);
                    ownerid.val(this.returnValue.split('|')[2]);
                }
            }
        }).show();
    });
    $(".ORGJobSearch").click(function () {
        var input = $(this).prev();
        var nextinput = $(this).next();
        var deptid = $("#deptid").val();
        dialog({
            id: 'Job-dialog ',
            title: 'Job 职位',
            url: "/SelectPage/ORGJob?Dpaymentid=" + deptid,
            onclose: function () {
                if (this.returnValue) {
                    nextinput.val(this.returnValue.split('|')[0]);
                    input.val(this.returnValue.split('|')[1]);
                }
            }
        }).show();
    });
    $(".AgentUserSearch").click(function () {
        var input = $(this).prev();
        var nextinput = $(this).next();
        dialog({
            id: 'User-dialog',
            title: 'User 用户',
            url: "/SelectPage/ORGUser",
            onclose: function () {
                if (this.returnValue) {
                    nextinput.val(this.returnValue.split('|')[2]);
                    input.val(this.returnValue.split('|')[1]);

                }
            }
        }).show();
    });
    $(".Chekvendor").change(function () {
        if ($(this).val() != "") {
            $.post("/SelectPage/GetCostvendor", {
                costvendor: $(this).val()
            }, function (data, status) {
                if (data == "False") {
                    updateAlert("The vendor is not active!！ 供应商编号填写错误！");
                    $(".Chekvendor").val("");
                    return false;
                } else {
                    return true;
                };
            });
        }
    });



    $("#hotellist").click(function () {
        dialog({
            title: 'Hotel List 酒店信息',
            url: "/SelectPage/Hotellist",
            width: 1000,
        }).show();
    });

    $("#exchangrate").click(function () {
        dialog({
            title: 'FX Rate 汇率信息',
            url: "/SelectPage/Ratelist",
            width: 800,
            height: 500,
        }).show();
    });
    $("#standard").click(function () {
        dialog({
            title: 'Standard Rate  报销标准',
            url: "/SelectPage/StandardRate",
        }).show();
    });
    $("#PettycashSearch").click(function () {
        var input = $(this).prev();
        var inputID = $(this).next();
        var thisObj = $(this);
        dialog({
            id: 'Pettycash-dialog',
            title: 'Pettycash 现金预支',
            url: "/SelectPage/Pettycash",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                    //input.val(this.returnValue);
                    var strprocessname = $("#processname").val();
                    if (strprocessname == "Proc_TravelExpense") {
                        Advance();
                    } else if (strprocessname == "Proc_CommonExpense") {
                        AdvanceCommon();
                    }
                }
                if (thisObj.hasClass("afterSearch")) {
                    AfterSearchAction();
                }
            }
        }).show();
    });
    $(".PayeeeSearch").click(function () {
        var input = $(this).prev().prev();
        var inputname = $("#bankname");
        var inputcode = $("#bankac");
        dialog({
            id: 'InternalBank-dialog',
            title: 'InternalBank  收款人',
            url: "/SelectPage/GetInternetBankAccount",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                    inputname.val(this.returnValue.split('|')[1]);
                    inputcode.val(this.returnValue.split('|')[2]);

                }
            }
        }).show();
    });
    $(".RefundSearch").click(function () {
        var input = $(this).prev().prev().prev();
        dialog({
            id: 'Refund-dialog',
            title: 'Refund  收款人',
            url: "/SelectPage/GtDeposit",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue);
                    input.blur();
                }
            }

        }).show();
    });


    $(".ExchangerateSearch").click(function () {
        var input = $(this).prev();
        var inputid = $(this).next();
        var curry = $("#currency").val();
        var curryy = $("#currency");
        dialog({
            id: 'rate-dialog',
            title: 'Exchangerate/汇率信息',
            url: "/SelectPage/exchangerate?curry=" + curry,
            onclose: function () {
                if (this.returnValue) {
                    inputid.val(this.returnValue.split("|")[0]);
                    input.val(this.returnValue.split("|")[1]);
                    var amount = $("#amount").val().replace(/,/g, "");
                    var fxrate = this.returnValue.split("|")[1];
                    if (!isNaN(amount) == true && amount != "") {
                        var cnyAmoun = (amount * fxrate).toFixed(2);
                        var money = $.formatMoney(cnyAmoun, 2);
                        $("#lbcnyamout").html(money);
                        $("#cnyamount").val(cnyAmoun);
                        var tramount = $("#tramount").val();
                        var cny = $("#cnyamount").val();
                        var cha = cny - tramount;

                        var secTxt = $("#secondaryexpensetype").children("option:selected").text();
                        if ((secTxt.indexOf("Hotel Cost") > -1 || secTxt.indexOf("酒店") > -1) && cha > 0) {
                            $("#explanation").addClass("required");
                        }
                        else { $("#explanation").removeClass("required") };
                    }
                    if (this.returnValue.split("|")[2] != null) {
                        curryy.val(this.returnValue.split("|")[2]);

                    }

                    if (curryy == 1) {
                        input.attr("disabled", true);
                    }

                }
            }
        }).show();

    });
    $(".FinanceCommentsSearch").click(function () {
        var input = $(this).prev();

        dialog({
            id: 'chartofaccount-dialog',
            title: 'chartofaccount  收入科目',
            url: "/SelectPage/GetFinance",
            onclose: function () {
                if (this.returnValue != null) {
                    input.val(this.returnValue);

                }
            }

        }).show();
    });
    $(".VendorSearch").click(function () {
        var input = $(this).prev();
        dialog({
            id: 'vendor-dialog',
            title: 'Vendor 供应商',
            url: "/SelectPage/Vendor",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split('|')[0]);
                }
                if (input.val() != null) {
                    $.get("/FormVendorAccountchanging/GetVendorByCode", { VendorCode: input.val() }, function (data) {
                        $("#companyname").val(data.vendornameen);
                        $("#companynamecn").val(data.vendornamecn);
                        $("#companyaddress").val(data.comaddressen);
                        $("#companyaddresscn").val(data.comaddresscn);
                        $("#banknumber").val(data.banknumber);
                        if (data.countrytype == 0) {
                            //$("#oversea").attr("checked", false);
                            $("#domestic").attr("checked", true);
                            $("#companyaddress").removeClass("required");
                            $("#companyname").removeClass("required");
                            $("#companynamecn").addClass("required");
                            $("#companyaddresscn").addClass("required");
                            $("#paymenttermid").val(2);
                            $("#vatregistrationno").addClass("required");
                            $("#countrytype").val(0);
                        }
                        else if (data.countrytype == 1) {
                            $("#oversea").attr("checked", true);
                            //$("#domestic").attr("checked", false);

                            $("#companynamecn").removeClass("required");
                            $("#companyaddresscn").removeClass("required");
                            $("#companyaddress").addClass("required");
                            $("#companyname").addClass("required");
                            $("#vatregistrationno").removeClass("required");
                            $("#paymenttermid").val("");
                            $("#countrytype").val(1);
                        }
                        $("#country").val(data.country);
                        $("#city").val(data.city);
                        $("#postcode").val(data.postcode);
                        $("#contactperson").val(data.contactpersoncn);
                        $("#contactpersoncn").val(data.contactpersonen);
                        $("#email").val(data.email);
                        $("#tel").val(data.tel);
                        $("#mobile").val(data.contactpersonmobile);
                        $("#fax").val(data.fax);
                        $("#nameofbank").val(data.bankname);
                        $("#bankaccount").val(data.bankaccount);
                        $("#vatregistrationno").val(data.vatregistrationno);
                        $("#swiftcode").val(data.swiftcode);
                        $("#paymentcurrencyid").val(data.paymentcurrencyid);
                        $("#iban").val(data.iban);
                        $("#paymentmethodid").val(data.paymentmethod_id);
                        $("#paymenttermid").val(data.paymenttermid);
                        $("#banknumber").val(data.banknumber);
                        //$("#typeofinvoiceid").val(data.invoicetype_id);
                        //$("#rateid").val(data.rate_id);
                        for (var i = 0; i < data.invoicetype_id.split(',').length; i++) {
                            $("input[name='checkItemInvoiceType']:checkbox").each(function () {
                                if (data.invoicetype_id.split(',')[i] == $(this).val()) {
                                    $(this).attr("checked", true);
                                }
                            });
                        }
                        for (var i = 0; i < data.rate_id.split(',').length; i++) {
                            $("input[name='checkItemRate']:checkbox").each(function () {
                                if (data.rate_id.split(',')[i] == $(this).val()) {
                                    $(this).attr("checked", true);
                                }
                            });
                        }
                        $("#communicationlanguageid").val(data.language_id);
                        $("#paymentterm").val($("#paymenttermid").children("option:selected").text());
                        $("#rate").val($("#rateid").children("option:selected").text());
                        $("#typeofinvoice").val($("#typeofinvoiceid").children("option:selected").text());
                        $("#paymentmethod").val($("#paymentmethodid").children("option:selected").text());
                        $("#paymentcurrency").val($("#paymentcurrencyid").children("option:selected").text());
                        $("#communicationlanguage").val($("#communicationlanguageid").children("option:selected").text());
                        //附件信息
                        $.get("/FormVendorAccountchanging/GetVendorByid", { vendorid: data.id, filetype: "others required" }, function (data) {
                            if (data != "") {
                                $("#Link5").attr("href", data.docurl);
                                $("#othersrequired").val(data.docname);
                                $("#othersrequiredurl").val(data.docurl);
                                $("#pic5").html(data.docname);
                                if ($("#delbtn5").length == "0") {
                                    $("#Link5").after("<i class='icon-trash icon-large delbtn del' id='delbtn5'></i>");
                                }
                                $("#delbtn5").bind("click", function () {
                                    $("#pic5").html("");
                                    $("#othersrequired").val("");
                                    $("#othersrequiredurl").val("");
                                    $("#delbtn5").remove();
                                });
                            } else {
                                $("#pic5").html("");
                                $("#othersrequired").val("");
                                $("#othersrequiredurl").val("");
                                $("#delbtn5").remove();
                            }
                        });
                        $.get("/FormVendorAccountchanging/GetVendorByid", { vendorid: data.id, filetype: "aic screenshot" }, function (data) {
                            if (data != "") {
                                $("#Link4").attr("href", data.docurl);
                                $("#aicscreenshotaic").val(data.docname);
                                $("#aicscreenshotaicurl").val(data.docurl);
                                $("#pic4").html(data.docname);
                                if ($("#delbtn4").length == "0") {
                                    $("#Link4").after("<i class='icon-trash icon-large delbtn del' id='delbtn4'></i>");
                                }
                                $("#delbtn4").bind("click", function () {
                                    $("#pic4").html("");
                                    $("#aicscreenshotaic").val("");
                                    $("#aicscreenshotaicurl").val("");
                                    $("#delbtn4").remove();
                                });
                            }
                            else {
                                $("#pic4").html("");
                                $("#aicscreenshotaic").val("");
                                $("#aicscreenshotaicurl").val("");
                                $("#delbtn4").remove();
                            }

                        });
                        $.get("/FormVendorAccountchanging/GetVendorByid", { vendorid: data.id, filetype: "tax certificate" }, function (data) {
                            if (data != "") {
                                $("#Link3").attr("href", data.docurl);
                                $("#taxcertificate").val(data.docname);
                                $("#taxcertificateurl").val(data.docurl);
                                $("#pic3").html(data.docname);
                                if ($("#delbtn3").length == "0") {
                                    $("#Link3").after("<i class='icon-trash icon-large delbtn del' id='delbtn3'></i>");
                                }
                                $("#delbtn3").bind("click", function () {
                                    $("#pic3").html("");
                                    $("#taxcertificate").val("");
                                    $("#taxcertificateurl").val("");
                                    $("#delbtn3").remove();
                                });
                            }
                            else {
                                $("#pic3").html("");
                                $("#taxcertificate").val("");
                                $("#taxcertificateurl").val("");
                                $("#delbtn3").remove();
                            }

                        });

                        $.get("/FormVendorAccountchanging/GetVendorByid", { vendorid: data.id, filetype: "bank opening license" }, function (data) {
                            if (data != "") {
                                $("#Link2").attr("href", data.docurl);
                                $("#bankopeninglicense").val(data.docname);
                                $("#bankopeninglicenseurl").val(data.docurl);
                                $("#pic2").html(data.docname);
                                if ($("#delbtn2").length == "0") {
                                    $("#Link2").after("<i class='icon-trash icon-large delbtn del' id='delbtn2'></i>");
                                }
                                $("#delbtn2").bind("click", function () {
                                    $("#pic2").html("");
                                    $("#bankopeninglicense").val("");
                                    $("#bankopeninglicenseurl").val("");
                                    $("#delbtn2").remove();
                                });
                            }
                            else {
                                $("#pic2").html("");
                                $("#bankopeninglicense").val("");
                                $("#bankopeninglicenseurl").val("");
                                $("#delbtn2").remove();
                            }

                        });

                        $.get("/FormVendorAccountchanging/GetVendorByid", { vendorid: data.id, filetype: "business license with company chop" }, function (data) {
                            if (data != "") {
                                $("#Link1").attr("href", data.docurl);
                                $("#businesslicense").val(data.docname);
                                $("#businesslicenseurl").val(data.docurl);
                                $("#pic1").html(data.docname);
                                if ($("#delbtn1").length == "0") {
                                    $("#Link1").after("<i class='icon-trash icon-large delbtn del' id='delbtn1'></i>");
                                }
                                $("#delbtn1").bind("click", function () {
                                    $("#pic1").html("");
                                    $("#businesslicense").val("");
                                    $("#businesslicenseurl").val("");
                                    $("#delbtn1").remove();
                                });
                            }
                            else {

                                $("#pic1").html("");
                                $("#businesslicense").val("");
                                $("#businesslicenseurl").val("");
                                $("#delbtn1").remove();
                            }

                        });
                    });
                }

            }
        }).show();
    });
    $("#Advancepayment").click(function () {
        var advancesorldid = $("#advancesorldid");
        var advancepaymentid = $("#advancepaymentid");
        var input = $(this).prev();
        var inputNo = $(this).next().next().next();
        var vendor = $("#vendorcode").val();
        var isadvendor = $("#isnewadvancepayment");
        var totalpayment = parseFloat($("#totalpayment").val());
        if (vendor == "") {
            updateAlert("Please fill in the !/请选择供应商编号！");
            return false;
        }
        dialog({
            id: 'Advancepayment-dialog',
            title: 'Advance Payment 预付款',
            url: "/SelectPage/AdvancePayment?vendor=" + vendor,
            onclose: function () {
                if (this.returnValue) {
                    $("#advancepaymentno").val("")
                    $("#adv").html("")
                    if (this.returnValue.split('|').length > 1) {
                        //冲销金额
                        input.val(this.returnValue.split('|')[0]);
                        // input.attr("disabled", false);
                        //最终付款金额
                        var finamount = totalpayment - input.val();
                        $("#finalpaymentamount").val(finamount);
                        var finmoney = $.formatMoney(finamount, 2);
                        $("#sement").html(finmoney);

                        //冲销单号
                        inputNo.val(this.returnValue.split('|')[1]);
                        $("#adv").html(this.returnValue.split('|')[1]);
                        $("#adv").next().val(this.returnValue.split('|')[1]);
                        isadvendor.val(this.returnValue.split('|')[2]);
                        //冲销单号id  
                        if (isadvendor.val() == 1) {
                            advancepaymentid.val(this.returnValue.split('|')[3]);
                        } else {
                            advancesorldid.val(this.returnValue.split('|')[3]);
                        }

                        AfterAction();
                    }

                }
            }
        }).show();

    });

    $(".CostReportSearch").click(function () {
        var input = $(this).prev();
        var inputid = $(this).next();
        dialog({
            id: 'CostReport-dialog',
            title: 'CostReport/会计科目',
            url: "/SelectPage/GetTravelcostReport",
            onclose: function () {
                if (this.returnValue) {
                    input.val(this.returnValue.split("|")[0]);
                    inputid.val(this.returnValue.split("|")[1]);
                }
            }
        }).show();
    });


    $(".btnLogDetail").click(function () {
        dialog({
            id: 'Log',
            title: 'Log Detail',
            url: $(this).attr("url"),
            width: '500px'
        }).show();
    });

    $(".ExpenseTypeSearch").click(function () {
        var input = $(this).prev();
        var costcenter = $("#costcenter");
        var intorderno = $("#intorderno");
        var expensetypeid = $("#expensetypeid");
        dialog({
            id: 'BdExpensetype-dialog',
            title: 'Expense Type 费用类型',
            url: "/SelectPage/GetBdExpensetype",
            width: '1000px',
            //height: '100%',

            onclose: function () {
                if (this.returnValue) {

                    input.val(this.returnValue.split('|')[0]);
                    expensetypeid.val(this.returnValue.split('|')[3]);
                    if (this.returnValue.split('|')[1] != "") {
                        costcenter.val(this.returnValue.split('|')[1]);
                    }
                    if (this.returnValue.split('|')[2] != "") {
                        intorderno.val(this.returnValue.split('|')[2]);
                    }
                }
            }
        }).show();
    });
    $("#help").click(function () {
        var strId = "";
        var strTitle = "";
        var strUrl = "";
        if ($("#processname").val() == "Proc_TravelRequest") {
            strId = "TravelRequest-dialog";
            strTitle = "Travel Request/差旅申请";
            strUrl = "/FormTravelRequest/Tips";
        }
        else if ($("#processname").val() == "Proc_TravelExpense") {
            strId = "TravelCostReport-dialog";
            strTitle = "Travel Cost Report/差旅报销";
            strUrl = "/FormTravelExpense/Tips";
        }
        else if ($("#processname").val() == "Proc_EntertainmentRequest") {
            strId = "EntertainmentRequest-dialog";
            strTitle = "Entertainment Request/招待费申请";
            strUrl = "/FormEntertainmentRequest/Tips";
        }
        else if ($("#processname").val() == "Proc_EntertainmentReport") {
            strId = "FormEntertainmentExpense-dialog";
            strTitle = "Entertainment Report/招待费报销申请";
            strUrl = "/FormEntertainmentExpense/Tips";
        }
        else if ($("#processname").val() == "Proc_PettyCash") {
            strId = "PettyCash-dialog";
            strTitle = "Petty Cash Application /现金预支";
            strUrl = "/FormPettyCash/Tips";
        }
        else if ($("#processname").val() == "Proc_CommonExpense") {
            strId = "CommomExpense-dialog";
            strTitle = "Common Expense Report/普通费用报销申请";
            strUrl = "/FormCommonExpense/Tips";
        }
        else if ($("#processname").val() == "Proc_AdvancePayment") {
            strId = "AdvancePayment-dialog";
            strTitle = "Down Payment Application/供应商预付款申请";
            strUrl = "/FormAdvancePayment/Tips";
        }
        else if ($("#processname").val() == "Proc_InvoicePaymentVerification") {
            strId = "InvoicePaymentVerification-dialog";
            strTitle = "Invoice Payment Verification/供应商发票付款申请";
            strUrl = "/FormInvoicePaymentVerification/Tips";
        }
        else if ($("#processname").val() == "Proc_SalesIncentivePayout") {
            strId = "SalesIncentivePayout-dialog";
            strTitle = "Sales Incentive Pay-out Application/销售激励支付申请单";
            strUrl = "/FormSalesIncentivePayout/Tips";
        }
        else if ($("#processname").val() == "Proc_PaymentRequest") {
            strId = "PaymentRequest-dialog";
            strTitle = "Other Payment Request/非费用付款";
            strUrl = "/FormPaymentRequest/Tips";
        }
        else if ($("#processname").val() == "Proc_InvoiceIssuance") {
            strId = "PaymentRequest-dialog";
            strTitle = "Invoice Issuance Application/公司开票申请单";
            strUrl = "/FormInvoiceIssuance/Tips";
        }
        else if ($("#processname").val() == "Proc_VendorAccountOpening") {
            strId = "VendorChange-dialog";
            strTitle = "New Vendor Opening/新供应商申请";
            strUrl = "/FormVendorAccountOpening/Tips";
        }
        else if ($("#processname").val() == "Proc_VendorAccountChanging") {
            strId = "VendorChange-dialog";
            strTitle = "Vendor Change/供应商信息变更申请";
            strUrl = "/FormVendorAccountchanging/Tips";
        } if (strId != "") {
            dialog({
                id: strId,
                title: strTitle,
                url: strUrl,
                width: 680,
                height: 550,
                onclose: function () {

                }
            }).show();
        }
    });

    $(".cleargroupvalue").click(function () {
        $(this).parent().children("input").first().val("");
    });
});


function savecheckform() {
    if (!$(".updatebtn").is(':hidden')) {
        $(".updatebtn").trigger("click");
    }
}
