// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Some semi convoluted Javascript to highlight the selected list in the sidebar
const highlightSelectSidebarList = () => {
    const listId = $(".list-id").attr("href");
    const pageTitle = $("#list-title").html();
    let userClickedOnList = false;
    $(".sidebar-link").filter(function () {
        if ($(this).attr("href") === listId) {
            $(this).addClass("bg-gray-50 font-semibold");
            userClickedOnList = true;
        }
    });

    // If the bool is false it means one of the top categories is selected, eg. Important
    if (!userClickedOnList) {
        // Get the closest h3 that matches the page title, select its parent li element and modify the class
        $(`.sidebar-link h3:contains(${pageTitle})`).closest("li").addClass("bg-gray-50 font-semibold");
    }
}
highlightSelectSidebarList();