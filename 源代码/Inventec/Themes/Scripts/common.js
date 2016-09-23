$.ajaxSetup ({
    cache: false //关闭AJAX相应的缓存
});
var _TEMPLET_DIR = "/themes/";
var lang = 0;
var _lang = 'en';
var Profile = {};
var ftp_path = {};
var nowPage = 1;
var im;
var settings = {};
var arrayObj = new Array();
var array = new Array();
settings.dpiX = 96;
settings.dpiY = 96;
settings.Angle = 90;

var query_now_page = 0;
var query_com_code;
var query_site_code;
var query_area_code;
var query_com_full_code;
var query_site_full_code;
var query_area_full_code;
var query_inv_number;
var query_inv_type;
var query_status;
var query_vendor_code;
var query_store_number;
var query_batch_from;
var query_batch_to;
var query_scan_from;
var query_scan_to;
var query_inv_date_from;
var query_inv_date_to;
var query_cert_data_from;
var query_cert_data_to;

var store_bureau_code;
var store_site_code;
var store_inv_number;
var store_is_store;
var store_certdatefrom;
var store_certdateto;
var store_now_page=0;
var store_certrst;

var cert_site_code;
var cert_result;
var cert_is_add;
var cert_now_page = 0;
var cert_gl_now_page = 0;
var cert_err_now_page = 0;

//var error_nowstatus; //批次页面当前选择的状态
var batch_nowcompany; //批次页面当前选择的公司代码
var batch_nowtype; //批次页面当前选择的发票类型
//var error_nowqrcode; //批次页面当前选择的条码号
var batch_nowscanfrom; //批次页面当前选择的时间from
var batch_nowscanto; //批次页面当前选择的时间to
//var Sqysqd_type; //批次页面发票类型
var batch_nowpage = 1; //批次页面当前选择的页码
var batch_vendorcode; //批次页面当前选择的供应商编号
var batch_nowinvfrom ;
var batch_nowsinvto ;
var Invoice_id = 0;
//获取项目跟路径
function getWebRootPath(){
	var path = location.href ;
	var pathArr = path.split("/");
	return pathArr[0]+"//"+pathArr[2]+'/';
}
function getUrl(_url){
    return getWebRootPath() + (_url.indexOf("/") == 0 ? _url.substring(1) : _url);
}

function loadfile(filename, filetype)
{
	if (filetype=="js"){ //判断文件类型
		var fileref=document.createElement("script")//创建标签
		fileref.setAttribute("type","text/javascript")//定义属性type的值为text/javascript
		fileref.setAttribute("src", getWebRootPath() + filename)//文件的地址
	}else if (filetype=="css"){ //判断文件类型
		var fileref=document.createElement("link")
		fileref.setAttribute("rel", "stylesheet")
		fileref.setAttribute("type", "text/css")
		fileref.setAttribute("href", filename)
	}
	if (typeof fileref!="undefined") {
		document.getElementsByTagName("head")[0].appendChild(fileref);
	}
}

function PageInit(lang) {
	_lang = (!lang || lang<=0) ? 'cn/' : 'en/';
	_TEMPLET_DIR = _TEMPLET_DIR + _lang;
	var main = $('#main');
	main.html('').load(_TEMPLET_DIR+"default.htm?"+Math.random());
	loadfile((!lang || lang<=0) ? 'js/zh-cn.js' : 'js/zh-en.js', 'js');
	var client = $(window).width();
	if(client > 1004) {
		main.css("width", "100%");
    }

    var dpi;
    if (Profile.screen_dpi != null) {
        var msg = Profile.screen_dpi.split('\"');
        if (msg[0] != null) {
            inch = msg[0];
        }
        if (msg[1] != null) {
            var str = msg[1].split('x');
            var x = str[0];
            var y = str[1];
        }
        dpi = Math.ceil(Math.sqrt(x * x + y * y) / inch);
    }
    settings.dpiX = dpi > 0 ? dpi : 96;
	settings.dpiY = dpi > 0 ? dpi : 96;
}

function getTotalWidth (){
	if($.browser.msie){ 
		return document.compatMode == "CSS1Compat"? document.documentElement.clientWidth : document.body.clientWidth; 
	} else{ 
		return self.innerWidth; 
	}
}

function getTotalHeight(){ 
	if($.browser.msie){ 
		return document.compatMode == "CSS1Compat"? document.documentElement.clientHeight : document.body.clientHeight; 
	}else { 
		return self.innerHeight; 
	}
}

var im;
function load(){
	im = Image(document.getElementById('testImg'));
	im.init();
}
function strToObj(str){  
	str = str.replace(/&/g,"','");  
	str = str.replace(/=/g,"':'");  
	str = "{'"+str+"'}";     
	return str;
}


function formInvnumber() {
	var S_INVNumber = $("#S_INVNumber").val();
	InvoiceCondition = "INVNumber="+S_INVNumber;
	document.getElementById('invoices').click();
	if($("#invoice_list").css("display") == 'none') {
		$("#invoice_list").css("display","inline");
	}
	document.getElementById('invoice_list').click();
}

//发票搜索
var InvoiceCondition = null;



function InvoiceFlag(k) {
    if ( k == 6) {
        return 0;
    } else if (k == 1 || k == 4 ) {
        return 1;
    } else if (k == 5 || k==0 || k==2||k==3) {
        return 2;
    } else {
        return 3;
        }
}

function TypeMessage(k) {
    var msg = new Array();
    msg[0] = new Array("未知", "增值税发票", "旧版运费发票", "新版运费发票", "销费清单");
    return msg[0][k];
}

function formatAmount(n, pad){
    if (!pad) pad = 2;
    if (n == "") {
        return "";
    }
	return n.toFixed(pad);
}

function UpLoad_Invoice(ftp_path) {

    var myDate = new Date();
    var upload_date = myDate.getFullYear() + "-" + (myDate.getMonth()+1) + "-" + myDate.getDate() + " " + myDate.toLocaleTimeString();
    $("#upload_user").html(Profile.full_name);
    $("#upload_date").html(upload_date);
    $("#up_Flag").html('<img src="' + getWebRootPath() + "images/" + InvoiceFlag(0) + '.gif" valign="absmiddle">上传，待处理');

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../BASF.asmx/Get_Up_Jpg",
        data: '{path:"' + ftp_path + '"}',
        dataType: 'json',
        success: function(msg) {
            if (msg) {
                var Img = $("<img src='" + getUrl(msg) + "'/>");
                Img.css("position", "absolute").easydrag();
                $("#invImg").html(Img);
            }
            $("#upload_item").show();
            $("#upload_detail").show();
            $("#Ensure").show();
        },
        error: function(x, e) {
            alert("error:" + x.responseText);
        }
    });
}

function SetInvoiceClean(flags) {
	if(!Invoice_Id || Invoice_Id <=0) return;
	$.ajax({
		type: "POST",
		contentType:"application/json; charset=utf-8",
		url:"../../BASF.asmx/UpdateInvIsClean",
		data: '{Invoice:"' + Invoice_Id + '", flags:"'+ flags +'"}',
		dataType:'json',
		success:function(msg){
			if(msg == 100){
				$("#MessageBox").html('');
			}else{
				alert(INVLang.invoice_clean_set_failed);
			}
		},
		error: function(x, e) {  
			alert("error:" + x.responseText);  
		}
	});
}

function SetStamp(flags) {
    if (!Invoice_Id || Invoice_Id <= 0) return;
    if (flags == 0) {
        $("#stamped").css("background-color", "#e6b9b8");
    }
    else {
        $("#stamped").css("background-color", "");
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../BASF.asmx/SetStamp",
        data: '{Invoice:"' + Invoice_Id + '", flags:"' + flags + '"}',
        dataType: 'json',
        success: function(msg) {
            if (msg == 100) {
                $("#MessageBox").html('');
            } else {
                alert(INVLang.invoice_clean_set_failed);
            }
        },
        error: function(x, e) {
            alert("error:" + x.responseText);
        }
    });
}

function ExportStatus(k) {
	var msg = new Array();
	msg[0] = new Array("失败", "成功");
	msg[1] = new Array("Failure", "Success");
	return msg[Profile.Language][k];
}


function DetailInvSave() {
    
    var text = DetailInv.CurrentRow.getValue();
    var res = eval('(' + text + ')');
    var html = new Array();
    html[html.length] = "inv_id:'" + Invoice_Id + "'";
    
    if (!res.id) {
        html[html.length] = "id:'-1'";
    }
    else {
        html[html.length] = "id:'" + res.id + "'"; 
    }
    
    html[html.length] = "item_name:'" + res.item_name + "'";
    html[html.length] = "item_unit:'" + res.item_unit + "'";
    html[html.length] = "item_count:'" + res.item_count + "'";
    var item = '{' + html.join(',') + '}';
   
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../BASF.asmx/DetailInvSave",
        data: item,
        dataType: 'json',
        success: function(msg) {
            if (!msg) alert("保存清单失败！！");
            else {
                alert("保存清单成功！！");
                clickRow(Invoice_Id);
            }
        },
        error: function(x, e) {
            alert("error:" + x.responseText);
        },
        complete: function(x) {
        }
    });
}
function DetailInvCancel() {
    DetailInv.CurrentRow.Cancel();
}
function DetailInvInsert() {
    DetailInv.insert(DetailInv.CurrentRow.rowIndex + 1);
}
function DetailInvDelete() {
    var id = DetailInv.CurrentRow.getColumn('id');
    if (!id || id <= 0) {
        DetailInv.remove(DetailInv.CurrentRow.rowIndex);
        DetailInv.CurrentRow.menu.innerHTML = '';
        return;
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../BASF.asmx/DetailInvDelete",
        data: '{id:' + id + '}',
        dataType: 'json',
        success: function(msg) {
            if (msg) {
                DetailInv.remove(DetailInv.CurrentRow.rowIndex);
                DetailInv.CurrentRow.menu.innerHTML = '';
            }
        },
        error: function(x, e) {
            alert("error:" + x.responseText);
        },
        complete: function(x) {
        }
    });
}

/*  
**    ====================================
**    类名：CLASS_LIANDONG_YAO  
**    功能：多级连动菜单  
**    作者：zqy    
**/
function CLASS_LIANDONG_YAO(array) {
    //数组，联动的数据源
    this.array = array;
    this.indexName = '';
    this.obj = '';
    //设置子SELECT
    // 参数：当前onchange的SELECT ID，要设置的SELECT ID
    this.subSelectChange = function(selectName1, selectName2) {
        //try
        //{
        var obj1 = document.all[selectName1];
        var obj2 = document.all[selectName2];
        var objName = this.toString();
        var me = this;
        obj1.onchange = function() {
            me.optionChange(this.options[this.selectedIndex].value, obj2.id)
        }
    }
    //设置第一个SELECT
    // 参数：indexName指选中项,selectName指select的ID
    this.firstSelectChange = function(indexName, selectName) {
        this.obj = document.all[selectName];
        this.indexName = indexName;
        this.optionChange(this.indexName, this.obj.id)
    }
    // indexName指选中项,selectName指select的ID
    this.optionChange = function(indexName, selectName) {
        var obj1 = document.all[selectName];
        var me = this;
        obj1.length = 0;
        obj1.options[0] = new Option("请选择", '');
        for (var i = 0; i < this.array.length; i++) {
            if (this.array[i][1] == indexName) {
                //alert(this.array[i][1]+" "+indexName);
                obj1.options[obj1.length] = new Option(this.array[i][2], this.array[i][0]);
            }
        }
    }
}

//鼠标浮动
function over() {
    document.body.style.cursor = "pointer";
}
//鼠标离开
function out() {
    document.body.style.cursor = "auto";
}

//写cookies 

function setCookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

//读取cookies 
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

//删除cookies 
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}



function emptyIf(str, t) {
    switch (typeof str) {
        case 'undefined': return t;
        case 'string': if (str.length == 0) return t;
        case 'object': if (null === str) return t;
    }
    return str;
}

function clickRow(ID,lang) {
//   // window.location.href = 'fpview.html?Invoice_ID=' + ID;
    window.open('fpview.html?Invoice_ID=' + ID+'&lang='+lang);
}

//判断用户权限
function GetUserPermission(permission) {
    var permissions = getCookie('Permissions');
    if (permissions == "all") {
        return true;
    }
    var data = eval('(' + permissions + ')');
    if (!data) {
        return false;
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].permission_id == permission) {
            return true;
        }
    }
    return false;
}

//数组去重复
function unique(arr) {
    var result = [], hash = {};
    for (var i = 0, elem; (elem = arr[i]) != null; i++) {
        if (!hash[elem]) {
            result.push(elem);
            hash[elem] = true;
        }
    }
    return result;
}

//解析Insight传入的字符参数  " LoginName ":" Brenda.Wu ","Lang":"EN","TokenCode":" CLwxVMdARu626vG0DiwEmA=="
function AnalyzeInsightArr() {

}

function renderTime(date) {
    var da = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
    return da.getFullYear() + "/" + (da.getMonth() + 1) + "/" + da.getDate() + " " + da.getHours() + ":" + da.getSeconds() + ":" + da.getMinutes();
}