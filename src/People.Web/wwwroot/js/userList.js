/**
 * Method to get users from API.
 * @function
 * @async
 * @param {any} requestData - Object with following field: 'BranchId'.
 * @returns users - [{id: int, firstName: 'string', lastName: 'string', branchId: int, positions: [{id: int, name: 'string'}], email: 'string', avatar: 'string'}]
 */
async function searchUsers(requestData) {
    return $.ajax({
        method: 'GET',
        url: '../api/User/GetUsers',
        data: requestData,
        datatype: 'json',
        async: true,
        error: function (response) {
            console.log(response);
        }
    });
}

/**
 * Method to set users table.
 * @function
 * @async
 * @param {string} tableBodyId - Table body ID.
 * @param {string} branchSelectorId - Branch selector ID.
 */
async function setUsersTable(tableBodyId, branchSelectorId) {
    const branchId = $('#' + branchSelectorId).val();

    let requestData;

    const parsedBranchId = parseInt(branchId, 10);

    if (isNaN(parsedBranchId)) {
        requestData = {};
    }
    else {
        requestData = {
            BranchId: parsedBranchId
        };
    }

    const table = $('#' + tableBodyId);
    table.empty();

    const users = await searchUsers(requestData);
    if (users.length == 0) {
        appendUserToTalbe(table, '', '', 'No users found', '');
        return;
    }
    
    $.each(users, function (){
        const elem = $(this)[0];
        const positions = elem.positions.map(function (position) { return position.name }).join(', ');
        const url = `Info?userId=${elem.id}`;
        appendUserToTalbe(table, elem.avatar, elem.fullName, url, positions);
    });
}

/**
 * Appends user to table.
 * @function
 * @param {any} table - DOM table.
 * @param {string} avatarUrl - Avatar URL.
 * @param {string} fullName - FullName.
 * @param {string} url - URL to user profile.
 * @param {string} positions - Positions.
 */

function appendUserToTalbe(table, avatarUrl, fullName, url, positions){
    table.append(`<tr><td><img class="class="img-fluid w-50 src="${avatarUrl}"/></td><td><a href="${url}">${fullName}</a></td><td>${positions}</td></tr>`);
}
