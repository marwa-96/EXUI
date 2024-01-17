
function checkImgCookies() {
    if (getCookies('setTourPM' + userID) == "true") {
        $('#TourPmImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourPmImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourAtt' + userID) == "true") {
        $('#TourAttImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourAttImg').attr('src', "/Content/dist/img/tourGid.gif");
    }

    if (getCookies('setTourTimeSheet' + userID) == "true") {
        $('#TourTimeSheetImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourTimeSheetImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourHomePage' + userID) == "true") {
        $('#TourHomeImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourHomeImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourDashboardEmp' + userID) == "true") {
        $('#TourEmpDashboardImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourEmpDashboardImg').attr('src', "/Content/dist/img/tourGid.gif");
    }

    if (getCookies('setTourDashboardAtt' + userID) == "true") {
        $('#TourDashbaordAttImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#TourDashbaordAttImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourPayroll' + userID) == "true") {
        $('#tourPayrollImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourPayrollImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourHR' + userID) == "true") {
        $('#tourHRImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourHRImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourCRM' + userID) == "true") {
        $('#tourCRMImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourCRMImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourAcc' + userID) == "true") {
        $('#tourAccImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourAccImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourInv' + userID) == "true") {
        $('#tourInvImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourInvImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourEvocher' + userID) == "true") {
        $('#tourEvocherImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourEvocherImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourVAS' + userID) == "true") {
        $('#tourVASImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourVASImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourAqr' + userID) == "true") {
        $('#tourAqrImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourAqrImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourPOS' + userID) == "true") {
        $('#tourPOSImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourPOSImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourCreatorRepor' + userID) == "true") {
        $('#tourCreatorReportImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourCreatorReportImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
    if (getCookies('setTourSysConfig' + userID) == "true") {
        $('#tourSysConfigImg').attr('src', "/Content/dist/img/tourguid.PNG");
    }
    else {
        $('#tourSysConfigImg').attr('src', "/Content/dist/img/tourGid.gif");
    }
}