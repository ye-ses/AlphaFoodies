
/*Runs when the is a type of staff selected*/
function hideContent(type)
{
    if (type === 'waiter') {
        
        document.getElementById('password_details').style.display = 'none';
    }
    else {
        document.getElementById('password_details').style.display = 'block';
    }
}


