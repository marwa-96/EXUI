//Start tour employee dashboard
var tourEmployeeDashboard = new Tour({
    steps: [{
        element: "#agesDashboardHelp",
        title: "",
        content: LocalResource('EmpHelp_EmployeesAges'),
        placement: "top"
    },
    {
        element: "#empWorkingHelp",
        title: "",
        content: LocalResource('EmpHelp_EmployeesWorkingType'),
        placement: "top"
    },
    {
        element: "#empTopTenDep",
        title: "",
        content: LocalResource('EmpHelp_TopTenDepartmentsEmployees'),
        placement: "top"
    },
   
{
    element: "#empExperienceYears",
    title: "",
    content: LocalResource('EmpHelp_EmployeesExperienceYears'),
    placement: "top"
}

,
{
    element: "#empTurnOverRatio",
    title: "",
    content: LocalResource('EmpHelp_TurnOverRatio'),
    placement: "top"
},

{
    element: "#empResignedAndHired",
    title: "",
    content: LocalResource('EmpHelp_ResignedAndHired'),
    placement: "top"
}
    ]
})

function employeeDashboardTour() {
    $('#TourEmpDashboardImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourDashboardEmp' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourCRM.end();
    tourEmployeeDashboard.init();
    tourEmployeeDashboard.restart();
}
//End tour employee dashboard

////Start tour find employee 
//function employeeFindEmployeeTour() {
//    tour.end();
//    tourAtt.end();
//    tourPM.end();
//    startTourPM.end();
//    tourEmployeeDashboard.end();
//    touremployeeFindEmployee.init();
//    touremployeeFindEmployee.start();

//}
//var touremployeeFindEmployee = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});
////End tour find employee 

////Start tour Add Employee
//function employeeAddEmployeeTour() {
//    tour.end();
//    tourAtt.end();
//    tourPM.end();
//    startTourPM.end();
//    tourEmployeeDashboard.end();
//    touremployeeFindEmployee.end();
//    tourEmployeeAddEmployee.init();
//    tourEmployeeAddEmployee.start();
//}

//var tourEmployeeAddEmployee = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});
////End tour Add Employee 


////start tour employee transform
//function employeeTransferEmployeeTour() {

//}
//var tourEmployeeTransferEmployee = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});

////end tour transfer

////start tour system users
//var tourEmployeeSystemUsers = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});
//function employeeSystemUsersTour() {

//}
////end tour system users

//// start tour employee organization chart
//function employeeOrganizationChartTour() {

//}
//var tourEmployeeOrganizationChart = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});

//// End tour employee organization chart

//// start tour employee Employees Report
//var touremployeeReports = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});
//function employeeReportsTour() {

//}
//// end tour employee Employees Report

//// start tour employee Settings
//function employeeSettingsTour() {

//}
//var touremployeeSettingd = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});
//// end tour employee Settings

//// Start tour change pass employee 
//function employeeChangePassTour() {

//}
//var touremployeeChangePass = new Tour({
//    steps: [
//      {
//          element: "",
//          title: "Title of my step",
//          content: "Content of my step",
//      },
//{
//    element: "",
//    title: "Title of my step",
//    content: "Content of my step",
//}
//    ]
//});

//// Start tour change pass employee 


