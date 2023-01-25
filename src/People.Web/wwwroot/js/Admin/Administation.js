/**
 * Function for update claim for user.
 * */
$(document).ready(function() {
    $(':checkbox[name=claimsEnabled]').on('change', function(){
        const request = {
            RoleId: $(this).attr('data-role-id'),
            ClaimValue: $(this).attr('data-claim'),
            IsEnabled: $(this).prop('checked'),
        }

        // TODO add jwt token to request.
        $.ajax({
            type: 'PUT',
            data: request,
            url: '/Administration/UpdateClaimsForRoles',
            error: function(){
                alert('Data not updated');
            }
        });
    });
});
