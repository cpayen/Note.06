// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Generic form modal
$('document').ready(function () {
    $('.ajax-modal-form-trigger').click(function () {
        let $this = $(this);
        $.get($this.data('url'))
            .done(function (result) {
                $('#formModal .modal-title').text($this.data('title'));
                $('#formModal .modal-body').html(result);
                $('#formModal [type="submit"]').attr('form', $('#formModal form').attr('id'));
                $('#formModal').modal('show');
            })
            .fail(function (erro) {
                console.log(error);
            });
    });
});