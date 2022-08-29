$(function ()
{
    $(document).on("click", ".confirmation", function ()
    {
        $("#confirmationModal .btn-danger").attr("href", $(this).attr("href"));
        $("#confirmationModal").modal("show");
        return false;
    });
})