$(function () {
$('#datetimepicker').datetimepicker({language: 'ru',
format: 'dd.mm.yyyy',
autoclose: true,
minView: 2,
weekStart: 1
});
$('#datetimepicker').datetimepicker('setMinutesDisabled', [0,59]);
$('#datetimepicker').datetimepicker('setHoursDisabled', [0,23]);
$.fn.datetimepicker.dates['ru-RU'] = {
    format: 'dd.mm.yyyy'
};

});