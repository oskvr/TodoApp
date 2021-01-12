// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function for highlighting the selected list in the sidebar
const highlightSelectSidebarItem = (() => {
    const listId = $(".list-id").attr("href");
    const selectedStyle = "bg-gray-50 text-blue-500 font-semibold";
    // If listId is not undefined it means a user created list is selected
    if (listId !== undefined) {
        $(".sidebar-link").filter(function () {
            if ($(this).attr("href") === listId) {
                $(this).addClass(selectedStyle);
                userClickedOnList = true;
            }
        });
    } else {
        // Get one of the top elements (eg. Important) from the current page title and select that
        const pageTitle = $("#list-title").html();
        const topCategory = $(`.sidebar-link h3:contains(${pageTitle})`);
        topCategory.closest("li").addClass(selectedStyle);

    }
})();

// Set all tooltips here
const createTooltips = (() => {
    const tooltip = (identifier, message) => {
        return tippy(identifier, {
            content: message,
            delay: [500, 0],
            inertia: true,
            animation: "scale-subtle",

        });
    }
    tooltip(".unchecked-star", "Mark as important");
    tooltip(".checked-star", "Mark as not important");
    tooltip(".task_list-link", "Go to list");
    tooltip(".uncompleted-task input", "Mark as completed");
    tooltip(".completed-task input", "Mark as not completed");
    tooltip(".btn-delete-task", "Delete task");
})();

// Any time a green success message is shown it will fade out after 4 seconds
const actionSuccessMessage = (() => {
    if ($("#validate-success").length) {
        setTimeout(function () {
            $("#validate-success").fadeOut(1000);
        }, 4000);
    }
})();
