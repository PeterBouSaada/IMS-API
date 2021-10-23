function ShowPage(index) {
    var userContainer = document.getElementById("user-container");
    var addContainer = document.getElementById("add-container");
    var editContainer = document.getElementById("edit-container");
    var deleteContainer = document.getElementById("delete-container");
    Pages = [userContainer, addContainer, editContainer, deleteContainer];
    for (i = 0; i < Pages.length; i++) {
        if (i == index) {
            if (index != 0) {
                Pages[i].style.display = "flex";
            }
            else {
                Pages[i].style.display = "block";
            }
            Pages[i].style.opacity = "1";
        }
        else {
            Pages[i].style.display = "none";
            Pages[i].style.opacity = "0";
        }
    }
}
