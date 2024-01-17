google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart1);
function drawChart1() {
    var data = google.visualization.arrayToDataTable([
      ['Month', 'Planned Cost', 'Acctual Cost', 'Planned revenue', 'Acctual revenue'],
      ['3', 44, 20, 90,88],
      ['5',60,55, 70, 52],
      ['10',40,10, 5, 8],
      ['25', 10, 66, 77, 99]
    ]);

    var options = {
        title: 'Campany Performance',
        hAxis: { title: 'Value', titleTextStyle: { color: 'red' } }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('chart_div1'));
    chart.draw(data, options);
}

google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart2);
function drawChart2() {
    var data = google.visualization.arrayToDataTable([
      ['Month', 'Sales', 'Expenses'],
      ['2013', 1000, 400],
      ['2014', 1170, 460],
      ['2015', 660, 1120],
      ['2016', 1030, 540]
    ]);

    var options = {
        title: 'Company Performance',
     
        vAxis: { minValue: 0 }
    };

    var chart = new google.visualization.AreaChart(document.getElementById('chart_div2'));
    chart.draw(data, options);
}

$(window).resize(function () {
    drawChart1();
    drawChart2();
});

// Reminder: you need to put https://www.google.com/jsapi in the head of your document or as an external resource on codepen //