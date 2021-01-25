window.ShowToastr = (type, message) => {
    toastr.options = {
        progressBar: true,
        newestOnTop: true,
        positionClass: "toast-bottom-right"
    };

    if (type === "success") {
        toastr.success(message, "Operation successful", { timeOut: 1000});
    }
    if (type === "error") {
        toastr.error(message, "Operation failed", { timeOut: 1000});
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success notification!',
            message,
            'success'
        )
    }
    if (type === "error") {
        Swal.fire(
            'Error notification!',
            message,
            'error'
        )
    }
}