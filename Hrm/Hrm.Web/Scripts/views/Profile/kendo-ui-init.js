$(function () {
    $('#dateOfBirthPicker').kendoDatePicker({
        format: "dd/MM/yyyy",
        parseFormats: ["MMMM yyyy", "dd.MM.yyyy"] //format also will be added to parseFormats
    });
    $("#totalWorkExpNumeric").kendoNumericTextBox();
});