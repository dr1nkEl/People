/** Function for interaction with modal window by ajax. */
$(document).ready(function () {
    /** Get modal window via ajax.
     * @param {string} url - url of page which must be loaded to modal window.
     * @param {string} title - title for modal window.
     * @returns {false}
     */
    window.jQueryModalGet = (url, title) => {
        $.ajax({
            type: 'GET',
            url: url,
            contentType: false,
            processData: false,
            success: function () {
                $('#form-modal .modal-body').load(url);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
            },
            error: function (err) {
                jqueryModalShowError(err);
            }
        })
        return false;
    }

    /** Post from modal window form via ajax.
     * @param {any} form - form from modal window.
     * @param {string} url - url of page which must be loaded after execution.
     * @returns {false}
     */
    window.jQueryModalPost = (form, listUrl) => {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#list').load(listUrl);
            },
            error: function (err) {
                jqueryModalShowError(err);
            },
        })
        return false;
        
    }

    /** Put from modal window form via ajax.
     * @param {any} form - form from modal window.
     * @param {string} url - url of page which must be loaded after execution.
     * @returns {false}
     */
    window.jQueryModalPut = (form, listUrl) => {
        $.ajax({
            type: 'PUT',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#list').load(listUrl);
            },
            error: function (err) {
                jqueryModalShowError(err);
            },
        })
        return false;
        
    }

    /** Delete from modal window form via ajax.
     * @param {any} form - form from modal window.
     * @param {string} url - url of page which must be loaded after execution.
     * @returns {false}
     */
    window.jQueryModalDelete = (deleteUrl, listUrl) => {
        $.ajax({
            type: 'DELETE',
            url: deleteUrl,
            success: function (res) {
                $('#list').load(listUrl);
            },
            error: function (err) {
                jqueryModalShowError(err);
            }
        })
        return false;
    }

    /** Display error in modal window.
     * @param {any} err - error.
     */
    window.jqueryModalShowError = (err) => {
        $('#form-modal .modal-body').html(err.responseText);
        $('#form-modal .modal-title').html('Error');
        $('#form-modal').modal('show');
    }

    /** Close modal window.
     * @returns {false}
     */
    window.closeWindow = () => {
        $('#form-modal').modal('hide');
        return false;
    }
});
