
function deleteConfirm1(obj){
    var choice = confirm('Are you sure!!!');
    var id1 = obj.id;
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

function deleteConfirmCourse(obj) {
    var choice = confirm('Are you sure!!!');
    var id1 = obj.id;
    if (choice == true) {
        $.ajax({
            url: '../../Admin/Course/Delete',
            method: 'GET',
            data: { courseId: id1 },
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


function deleteConfirmCategory(obj) {
    var choice = confirm('Are you sure!!!');
    var id1 = obj.id;
    if (choice == true) {
        $.ajax({
            url: '../../Admin/Categories/Delete',
            method: 'GET',
            data: { categoryId: id1 },
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



function deleteConfirm(obj) {
    var choice = confirm('Are you sure!!!');
    var id = obj.id;
    if (choice == true) {
        $.ajax({
            url: '../../Home/DeleteCourse',
            method: 'GET',
            data: { courseId: id },
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