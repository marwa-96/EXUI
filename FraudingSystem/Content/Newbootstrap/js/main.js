var check1 = document.getElementById("check1")
var check2 = document.getElementById("check2")
var page2 = document.getElementById("page2")
var formDetails = document.getElementById("formDetails")
var welcomePage = document.getElementById("welcomePage")
var selectOption=document.getElementById("inputGroupSelect04")
var employeeDetails=document.getElementById("employeeDetails")
var customerDetails=document.getElementById("customerDetails")
var nonCustomerDetails = document.getElementById("nonCustomerDetails")
var employee = document.getElementById("employee")
var LblCase = document.getElementById("LblCase")
var flexCheckChecked = document.getElementById("flexCheckChecked")
var closeEl = document.getElementById("closeEl")
var UserInfo = document.getElementById("UserInfo")
$('.textValidate').prop('required', true);
$("#UserInfo").hide();


function welcomButton() {
    {
        welcomePage.classList.add("d-none")
        page2.classList.remove("d-none")
        page2.classList.add("d-block")
    }
}
function checkedButton()
{
    if (check1.checked  && check2.checked ) {
        //page1.classList.remove("d-block")
       //page1.classList.add("d-none")
        formDetails.classList.remove("d-none")
        formDetails.classList.add("d-block")
        
        console.log("hhhh")
    }
}
function backButton() {
    welcomePage.classList.add("d-block")
    welcomePage.classList.remove("d-none")
    $("#check1").prop('checked', false);
    $("#check2").prop('checked', false);
    page2.classList.add("d-none")
    formDetails.classList.add("d-none")
}
function validName() {
    var regexName = /^[A-Z}[a-z]{3,15}[1-9]{0,3}$/
    if (regexName.test(LblCase.value) == true) {
        document.getElementById("alertName").addClass("d-none")
        document.getElementById("alertName").removeClass("d-block")
    }
    else {
        document.getElementById("alertName").addClass("d-block")
        document.getElementById("alertName").removeClass("d-none")
    }
}
closeEl.addEventListener("click", closeModel)
function closeModel() {
    UserInfo.style.display = "none"
}




//function displaySelected(){
//     var value =selectOption.value;
//    if (value ==1){
//            employeeDetails.classList.remove("d-none")
//             customerDetails.classList.add("d-none")
//             nonCustomerDetails.classList.add("d-none")   
//    }
//    else if(value==2){
//         customerDetails.classList.remove("d-none")
//         employeeDetails.classList.add("d-none")
//         nonCustomerDetails.classList.add("d-none")
//    }
//    else if(value==3){
//           employeeDetails.classList.add("d-none")
//           customerDetails.classList.add("d-none")
//           nonCustomerDetails.classList.remove("d-none")
//    }
//}
