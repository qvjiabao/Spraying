$(function () {
            loadControl = function (formName) {
                var list = getdata.getAccountTypeList();
                var NameList = list.NameList;
                var ValidateList = list.ValidateList;
                var AlertList = list.AlertList;

                for (var i = 0; i < NameList.length; i++) {
					bindInput(NameList[i],ValidateList[i],AlertList[i]);
                }
            }
			
			function bindInput(controlName,regexArr,msgArr){
				if($("input#"+controlName)){
					var control = $("#"+NameList[i]);
					control.focus(function(){
    					//validate(control.val(),regexArr,msgArr);
  					});
					control.blur(function(){
						//validate(control.val(),regexArr,msgArr);
					});
					control.change(function(){
						validate(control.val(),regexArr,msgArr);
					});
				}
			}
			
			function validate (value,regexArr,msgArr){
				for (var i = 0; i < regexArr.length; i++) {
					retex = "/"+regexArr[i]+"/";
					if(!regex.test(value)){
						return msg[i];
					}else{
						return true;	
					}
                }
			}
        });