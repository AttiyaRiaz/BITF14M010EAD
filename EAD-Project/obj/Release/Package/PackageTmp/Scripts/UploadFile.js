<script type="text/javascript">
    $(document).ready(function() {
        $('#upload').click(function () {
            var data = new FormData();
            var file = $('form input[type=file]')[0].files[0];
            data.append('file',file);
            $.ajax({
                url: '/Api/File/Upload',
                processData: false,
                contentType: false,
                data: data,
                type: 'POST'
            }).done(function(result) {
                alert(result);
            }).fail(function(a, b, c) {
                console.log(a, b, c);
            });
        });
 });