
var tourHR = new Tour({
    steps: [{
        element: "#divChartAvgSalaries",
        title: "",
        content: LocalResource('Help_Welcome'),
        placement: "top"
    }
    ]
})
function HRTour() {
    $('#tourHRImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourHR' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourCRM.end();
    tourHR.init();
    tourHR.restart();
}