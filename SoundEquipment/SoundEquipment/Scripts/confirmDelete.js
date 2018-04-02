function confirmDelete() {
    if (confirm("Are you sure you want to delete this user?")) {

        @Html.ActionLink("Delete", "DeleteUser", "User", new { userID = presentationObject.UserID }, null)
    }

    else {
        txt = "Delete cancelled."
    }
}