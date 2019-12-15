// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Generic form modal
$('document').ready(function () {
    $('.ajax-modal-form-trigger').click(function () {
        let $this = $(this);
        $.get($this.data('url'))
            .done(function (result) {

                $('#formModal .modal-dialog')
                    .removeClass('modal-sm')
                    .removeClass('modal-lg')
                    .removeClass('modal-xl');

                $('#formModal .modal-dialog').addClass($this.data("modalSize"));

                $('#formModal .modal-title').text($this.data('title'));
                $('#formModal .modal-body').html(result);

                $('#formModal [type="submit"]').attr('form', $('#formModal form').attr('id'));
                $.validator.unobtrusive.parse($('#formModal form'));

                $('#formModal').modal('show');
            })
            .fail(function (error) {
                console.log(error);
            });
    });
});