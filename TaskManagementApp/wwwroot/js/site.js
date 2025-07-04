// Traditional AJAX call with jQuery
   
$(document).on('click', '.toggleStatus', function () {
    const taskId = $(this).val();
    const isChecked = $(this).is(':checked');
    const label = $(this).next('label');
    const $row = $(this).closest('tr');
    const apiBaseUrl = $('#apiBaseUrl').val();   
        
        $.ajax({
            url: `${apiBaseUrl}/tasks/${taskId}/toggle`,
            method: 'PUT',
            success: function () {               
                $row.fadeOut(200, function () {
                    $row.toggleClass('table-success');                  
                    $(label).text(isChecked ? 'Completed' : 'Pending');
                    $row.fadeIn(200);
                });
            }
        });
    });
