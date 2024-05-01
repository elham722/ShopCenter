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


//------------------------User-------------------------------------

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




function addUser(e) {
    e.preventDefault();
    var emailtitle = document.getElementById("Email").value;
    if (!emailtitle) {
        Swal.fire({
            title: "خطا",
            text: "  ایمیل را وارد کنید",
            icon: "error",
            confirmButtonText: "تایید"
        });
        return;
    }
    const form = new FormData(document.getElementById("frmCreateUser"));
    var input = document.getElementById("UserAvatar");
    const file = input.files[0];
    form.append('UserAvatar', file);

    $.ajax({
        url: "/submitaddusermodal",
        type: "post",
        data: form,
        processData: false, 
        contentType: false,  
        beforeSend: function () {
   
        },
        success: function (response) {
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
           
        },
        error: function () {
            Swal.fire({
                title: 'خطا!',
                text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
                icon: 'error',
                confirmButtonText: 'اوکی'
            });
        }
    });
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


function EditFormSubmited(e) {
    e.preventDefault();
    const form = new FormData(document.getElementById("frmEditUser"));
    var input = document.getElementById("UserAvatar");
    const file = input.files[0];
    form.append('UserAvatar', file);
 $.ajax({
        url: "/SubmitEditUser",
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
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

        },
  error: function () {
                Swal.fire({
                    title: 'خطا!',
                    text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
                    icon: 'error',
                    confirmButtonText: 'اوکی'
                });
            }
        });
}

function DeleteUser(UserId) {
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


//------------------------Category-------------------------------------

function loadCategoryModalBody() {
    $.ajax({
        url: "/addnewcategory",
        type: "get",
        data: {

        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#AddCategoryContent").html(response);

            $('#frmNewCategory').data('validator', null);
            $.validator.unobtrusive.parse('#frmNewCategory');

            $("#modalAddCategory").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}


function addCategory(e) {
    e.preventDefault();
    // اعتبارسنجی عنوان دسته بندی
    var categoryTitle = document.getElementById("CategoryTitle").value;
    if (!categoryTitle) {
        Swal.fire({
            title: "خطا",
            text: "عنوان دسته بندی را وارد کنید",
            icon: "error",
            confirmButtonText: "تایید"
        });
        return;
    }
    const form = new FormData(document.getElementById("frmNewCategory"));
    var input = document.getElementById("ImageUrl");
    const file = input.files[0];
    form.append('ImageUrl', file);

    $.ajax({
        url: "/submitaddnewcategory",
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        beforeSend: function () {
            
        },
        success: function (response) {
            CloseLoading();
            if (response.status === "Success") {
                Swal.fire({
                    title: "  موفق",
                    text: "دسته بندی جدید با موفقیت ثبت شد",
                    icon: "success"
                }).then((result) => {
                    if (result.isConfirmed) {
                        $("#modalAddCategory").modal("hide");

                        $.ajax({
                            url: location.href,
                            type: "GET",
                            success: function (data) {
                                $("#againloadrow").html($(data).find("#againloadrow").html());
                            }
                        });
                    }
                });
            }  else if (response.status === "ImageSizeInvalid") {
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

        },
        error: function () {
            Swal.fire({
                title: 'خطا!',
                text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
                icon: 'error',
                confirmButtonText: 'اوکی'
            });
        }
    });
}


function DeleteCategory(categoryId) {
    Swal.fire({
        title: "آیا از حذف دسته بندی مطمعن هستید؟",
        text: "در صورت حذف دسته بندی امکان برگشت وجود ندارد!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله!حدف کن"
    }).then((willDelete) => {
        if (willDelete.isConfirmed) {
            $.ajax({
                url: "/DeleteCategory",
                type: "GET",
                data: { CategoryId: categoryId },
                beforeSend: function () {
                    StartLoading();
                },
                success: function (response) {
                    CloseLoading();
                    if (response.status === "Success") {
                        Swal.fire({
                            title: "حذف شد!",
                            text: "دسته بندی با موفقیت حذف شد",
                            icon: "success"
                        });
                        $(`#Category-${categoryId}`).remove();
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



function loadAddSubCategoryModalBody(CategoryId) {
    $.ajax({
        url: "/AddSubNewCategory",
        type: "get",
        data: {
            categoryId: CategoryId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#AddSubCategoryContent").html(response);

            $('#frmNewSubCategory').data('validator', null);
            $.validator.unobtrusive.parse('#frmNewSubCategory');

            $("#modalAddSubCategory").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}


function addSubCategory(e) {
    e.preventDefault();
    // اعتبارسنجی عنوان دسته بندی
    var categoryTitle = document.getElementById("CategoryTitle").value;
    if (!categoryTitle) {
        Swal.fire({
            title: "خطا",
            text: "عنوان دسته بندی را وارد کنید",
            icon: "error",
            confirmButtonText: "تایید"
        });
        return;
    }
    const form = new FormData(document.getElementById("frmNewSubCategory"));
    var input = document.getElementById("ImageUrl");
    const file = input.files[0];
    form.append('ImageUrl', file);

    $.ajax({
        url: "/SubmitAddSubNewCategory",
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
            CloseLoading();
            if (response.status === "Success") {
                Swal.fire({
                    title: "  موفق",
                    text: "دسته بندی جدید با موفقیت ثبت شد",
                    icon: "success"
                }).then((result) => {
                    if (result.isConfirmed) {
                        $("#modalAddSubCategory").modal("hide");

                        $.ajax({
                            url: location.href,
                            type: "GET",
                            success: function (data) {
                                $("#againloadrow").html($(data).find("#againloadrow").html());
                            }
                        });
                    }
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

        },
        error: function () {
            Swal.fire({
                title: 'خطا!',
                text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
                icon: 'error',
                confirmButtonText: 'اوکی'
            });
        }
    });
}


function loadEditCategoryModalBody(CategoryId) {
    $.ajax({
        url: "/EditCategory",
        type: "get",
        data: {
            categoryId: CategoryId
        },
        beforeSend: function () {
            StartLoading();
        },
        success: function (response) {
            CloseLoading();
            $("#EditCategoryContent").html(response);

            $('#frmEditCategory').data('validator', null);
            $.validator.unobtrusive.parse('#frmEditCategory');

            $("#modalEditCategory").modal("show");
        },
        error: function () {
            CloseLoading();
        }
    });
}


function EditCategory(e) {
    e.preventDefault();
    // اعتبارسنجی عنوان دسته بندی
    var categoryTitle = document.getElementById("CategoryTitle").value;
    if (!categoryTitle) {
        Swal.fire({
            title: "خطا",
            text: "عنوان دسته بندی را وارد کنید",
            icon: "error",
            confirmButtonText: "تایید"
        });
        return;
    }
    const form = new FormData(document.getElementById("frmEditCategory"));
    var input = document.getElementById("ImageUrl");
    const file = input.files[0];
    form.append('ImageUrl', file);

    $.ajax({
        url: "/SubmitEditCategory",
        type: "post",
        data: form,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
            CloseLoading();
            if (response.status === "Success") {
                Swal.fire({
                    title: "  موفق",
                    text: "دسته بندی  با موفقیت ویرایش شد",
                    icon: "success"
                }).then((result) => {
                    if (result.isConfirmed) {
                        $("#modalEditCategory").modal("hide");

                        $.ajax({
                            url: location.href,
                            type: "GET",
                            success: function (data) {
                                $("#againloadrow").html($(data).find("#againloadrow").html());
                            }
                        });
                    }
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

        },
        error: function () {
            Swal.fire({
                title: 'خطا!',
                text: 'متاسفانه خطایی رخ داده است دوباره تلاش کنید',
                icon: 'error',
                confirmButtonText: 'اوکی'
            });
        }
    });
}

function clearSearchFieldCategory() {
    document.getElementsByName('categoryTitle')[0].value = '';
  
    document.forms[0].submit();
}
