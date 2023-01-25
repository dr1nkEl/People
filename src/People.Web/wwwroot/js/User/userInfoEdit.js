/** To show or hide section on document.
 * @param {string} sectionId - Section ID.
 */
function showOrHideSection(sectionId) {
    const section = document.getElementById(sectionId);
    if (section.hidden) {
        section.hidden = false
    } else {
        section.hidden = true;
    }
}

/** Format user field to HTML
 * @param {any} user - position.
 * @returns {any} position HTML element.
 */
function formatUsers(user) {
    $('#positionsSelector').attr('style', 'width: 75%');
    if (!user.id) {
        return user.text;
    }
    const $user = $(
        '<span><img/><span></span></span>'
    )
    $user.find('span').text(user.text);
    $user.find('img').attr('src', user.element.dataset.avatar);
    return $user;
};

/** Format position field to HTML
 * @param {any} position - position.
 * @returns {any} position HTML element.
 */
function formatPositions(position) {
    if (!position.id) {
        return position.text;
    }
    const $position = $(
        '<span><span></span></span>'
    )
    $position.find('span').text(position.text);
    return $position;
}

/**Configure select2*/
$(function () {
    $('#reportingUsersSelector').select2({
        templateResult: formatUsers,
        templateSelection: formatUsers,
    });
    $('#positionsSelector').select2({
        templateResult: formatPositions,
        templateSelection: formatPositions,
    });
})
