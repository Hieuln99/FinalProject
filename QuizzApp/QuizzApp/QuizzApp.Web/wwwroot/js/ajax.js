
function deleteConfirm(){
    var choice = confirm('Are you sure!!!');
    var id1 = document.getElementById('id2').innerHTML;
if(choice == true){
    $.ajax({
        url: 'https://quizizz.com:8181/Admin/Questions/DelAjax',
        method: 'GET',
        data: { questionId: id1 },
        success: function (res) {
            alert("Success.")
            location.reload();
        },
        error: function (res) {
            alert('failure!!!');
            console.log(res)
        }
    })
}
else{
        location.reload();
    }
}

function deleteConfirmCourse() {
    var choice = confirm('Are you sure!!!');
    var id1 = document.getElementById('id').innerHTML;
    if (choice == true) {
        $.ajax({
            url: 'https://quizizz.com:8181/Admin/Course/Delete',
            method: 'GET',
            data: { questionId: id1 },
            success: function (res) {
                alert("Success.")
                location.reload();
            },
            error: function (res) {
                alert('failure!!!');
                console.log(res)
            }
        })
    }
    else {
        location.reload();
    }
}