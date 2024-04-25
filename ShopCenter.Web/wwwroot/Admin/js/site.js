function StartLoading(element = 'body') {
    $(element).waitMe({

        effect: 'bounce',

        text: 'لطفا منتظر بمانید',

        bg: 'rgba(255, 255, 255, 0.7)',

        color: ' #0F4C81'

    });
}

function CloseLoading(element = 'body') {
    $(element).waitMe('hide');
}


function LoadCreateUserModalBody() {
    $.ajax({
        url: "/createuser",
        type: "get",
        data: {

        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#AddUserModalContent").html(response);

            $('#frmCreateUser').data('validator', null);
            $.validator.unobtrusive.parse('#frmCreateUser');

            $("#modalAddUser").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}

function AddUserSubmitedFormSubmited(response) {
    CloseLoading();
  if (response.status === "Success") {
        Swal.fire({
            title: "  موفق",
            text: "کاربر با موفقیت ثبت شد",
            icon: "success"
        }).then((result) => {
            if (result.isConfirmed) {
                $("#modalAddUser").modal("hide");
                
                $.ajax({
                    url: location.href,
                    type: "GET",
                    success: function (data) {
                        $("#againloadrow").html($(data).find("#againloadrow").html());
                    }
                });
            }
        });
    } else if (response.status === "DuplicateEmail") {
        Swal.fire({
            title: "خطا",
            text: "ایمیل وارد شده تکراری میباشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    } else if (response.status === "DuplicatePhoneNumber") {
        Swal.fire({
            title: "خطا",
            text: "شماره تماس وارد شده تکراری میباشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    } else if (response.status === "ImageSizeInvalid") {
        Swal.fire({
            title: "خطا",
            text: "حجم عکس  باید کمتر از 2 مگا بایت باشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    }
    else if (response.status === "ImageExensionInvalid") {
        Swal.fire({
            title: "خطا",
            text: "پسوند عکس نامعتبر است",
            icon: "error",
            confirmButtonText: "تایید"
        });
    }
    else {
        Swal.fire({
            title: 'خطا!',
            text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
            icon: 'error',
            confirmButtonText: 'اوکی'
        });
    }
}

function LoadDetailUserModalBody(userId) {
    $.ajax({
        url: "/Details",
        type: "get",
        data: {
            UserId: userId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#DetailsUserModalContent").html(response);

            $('#DetailsUserModal').data('validator', null);
            $.validator.unobtrusive.parse('#DetailsUserModal');

            $("#DetailsUserModal").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}

function LoadEditUserModalBody(userId) {
    $.ajax({
        url: "/Edit",
        type: "get",
        data: {
            UserId: userId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#EditUserModalContent").html(response);

            $('#modalEditUser').data('validator', null);
            $.validator.unobtrusive.parse('#modalEditUser');

            $("#modalEditUser").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}


function EditFormSubmited(response) {
    CloseLoading();
    if (response.status === "Success") {
        Swal.fire({
            title: "  موفق",
            text: "اطلاعات کاربر با موفقیت ویرایش شد",
            icon: "success"
        }).then((result) => {
            if (result.isConfirmed) {
                $("#modalEditUser").modal("hide");
                
                $.ajax({
                    url: location.href,
                    type: "GET",
                    success: function (data) {
                        $("#againloadrow").html($(data).find("#againloadrow").html());
                    }
                });
            }
        });
    } else if (response.status === "DuplicateEmail") {
        Swal.fire({
            title: "خطا",
            text: "ایمیل وارد شده تکراری میباشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    } else if (response.status === "UserNotFound") {
        Swal.fire({
            title: "خطا",
            text: "کاربری با مشخصات وارد شده یافت نشد!",
            icon: "error",
            confirmButtonText: "تایید"
        });
    } else if (response.status === "DuplicatePhoneNumber") {
        Swal.fire({
            title: "خطا",
            text: "شماره تماس وارد شده تکراری میباشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    } else if (response.status === "ImageSizeInvalid") {
        Swal.fire({
            title: "خطا",
            text: "حجم عکس  باید کمتر از 2 مگا بایت باشد",
            icon: "error",
            confirmButtonText: "تایید"
        });
    }
    else if (response.status === "ImageExensionInvalid") {
        Swal.fire({
            title: "خطا",
            text: "پسوند عکس نامعتبر است",
            icon: "error",
            confirmButtonText: "تایید"
        });
    }
    else {
        Swal.fire({
            title: 'خطا!',
            text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
            icon: 'error',
            confirmButtonText: 'اوکی'
        });
    }
}

function DeleteProduct(UserId) {
    Swal.fire({
        title: "آیا از حذف کاربر مطمعن هستید؟",
        text: "در صورت حذف کاربر امکان برگشت کاربر وجود ندارد!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله!حدف کن"
    }).then((willDelete) => {
        if (willDelete.isConfirmed) {
            $.ajax({
                url: "/delete-user",
                type: "GET",
                data: { UserId: UserId },
                beforeSend: function () {
                    StartLoading();
                },
                success: function (response) {
                    CloseLoading();
                    if (response.status === "Success") {
                        Swal.fire({
                            title: "حذف شد!",
                            text: "کاربر با موفقیت حذف شد",
                            icon: "success"
                        });
                        $(`#User-${UserId}`).remove();
                        $.ajax({
                            url: location.href,
                            type: "GET",
                            success: function (data) {
                                $("#refreshtable").html($(data).find("#refreshtable").html());
                            }
                        }); 
                    } else {
                        swal("خطا", "خطایی رخ داده است دوباره تلاش کنید", "error");
                    }
                },
                error: function () {
                    CloseLoading();
                    swal("خطا", "خطایی رخ داده است دوباره تلاش کنید", "error");
                }
            });
        }
    });
}



    function clearSearchFields() {
        document.getElementsByName('fullName')[0].value = '';
    document.getElementsByName('phoneNumber')[0].value = '';
    document.getElementsByName('email')[0].value = '';

 
    document.forms[0].submit();
    }



