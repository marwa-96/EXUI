
var tourPayroll = new Tour({
    steps: [{
        element: "#AvgSalariesMonth",
        title: "",
        content: LocalResource('AvgSalariesMonth'),
        placement: "top"
    },
    {
        element: "#AvgTopSalaryDep",
        title: "",
        content: LocalResource('AvgTopSalaryDep'),
        placement: "top"
    },
    {
        element: "#AvgTopSalaryJob",
        title: "",
        content: LocalResource('AvgTopSalaryJob'),
        placement: "top"
    },
    {
        element: "#AvgTopSalaryUnit",
        title: "",
        content: LocalResource('AvgTopSalaryUnit'),
        placement: "top"
    },
    {
        element: "#DeductionVsOvertime",
        title: "",
        content: LocalResource('DeductionVsOvertime'),
        placement: "top"
    },
    {
        element: "#DelayVsPenality",
        title: "",
        content: LocalResource('DelayVsPenality'),
        placement: "top"
    }
    ]
})
function payrollTour() {
    $('#tourPayrollImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourPayroll' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourHR.end();
    tourCRM.end();
    tourPayroll.init();
    tourPayroll.restart();
}