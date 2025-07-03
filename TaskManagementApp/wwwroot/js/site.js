//// Traditional AJAX call with jQuery
//function getTasksSummary(callback) {
//    $.ajax({
//        url: '/api/tasks/summary',
//        method: 'GET',
//        success: function (data) {
//            callback(data);
//        },
//        error: function (error) {
//            console.error('Error fetching task summary:', error);
//        }
//    });
//}

//// Initialize when DOM is ready
//$(function () {
//    // Load task summary
//    getTasksSummary(function (summary) {
//        console.log('Task summary:', summary);
//        // You could display this in a jQuery UI element
//    });

//    // Example of jQuery interactive feature
//    $(document).on('click', '.quick-complete', function () {
//        const taskId = $(this).data('task-id');
//        const $row = $(this).closest('tr');

//        $.ajax({
//            url: `/api/tasks/${taskId}/toggle`,
//            method: 'POST',
//            success: function () {
//                $row.fadeOut(200, function () {
//                    $row.toggleClass('table-success');
//                    $row.find('.task-status').text(function (_, text) {
//                        return text === 'Pending' ? 'Completed' : 'Pending';
//                    });
//                    $row.fadeIn(200);
//                });
//            }
//        });
//    });
//});