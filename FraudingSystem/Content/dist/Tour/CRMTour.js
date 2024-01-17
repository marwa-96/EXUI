var tourCRM = new Tour({
    //steps: [{
    //    element: "#divChartAvgSalaries",
    //    title: "",
    //    content: LocalResource('Help_Welcome'),
    //    placement: "top"
    //}
    //]
})

function CRMTour() {
    $('#tourCRMImg').attr('src', "../Content/dist/img/tourguid.PNG");
    setCookies('setTourCRM' + userID, 'true');
    tour.end();
    tourAtt.end();
    tourPM.end();
    tourTimeSheet.end();
    tourDashboardAtt.end();
    tourEmployeeDashboard.end();
    tourHR.end();
    tourPayroll.end();
    tourCRM.init();
    tourCRM.restart();
}