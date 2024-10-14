<script>
    $(document).ready(function () {
        $('#loginModal').on('show.bs.modal', function () {
            $.ajax({
                url: '@Url.Action("Login", "Account")',
                type: 'GET',
                success: function (result) {
                    $('#loginFormContainer').html(result);
                }
            });
        });

    // Handle form submission inside the modal
    $(document).on('submit', '#loginForm', function (e) {
        e.preventDefault();
    var form = $(this);
    $.ajax({
        url: form.attr('action'),
    type: form.attr('method'),
    data: form.serialize(),
    success: function (response) {
                        if (response.success) {
        location.reload(); // Reload page after successful login
                        } else {
        $('#loginFormContainer').html(response); // Replace with form including validation errors
                        }
                    }
                });
            });
        });
</script>