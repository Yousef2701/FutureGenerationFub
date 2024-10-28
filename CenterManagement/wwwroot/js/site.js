function search() {
    let search = document.getElementById('search').value.toLowerCase();
    let center = document.querySelectorAll('#center');
    for (let i = 0; i < center.length; i++) {
        let a = center[i].getElementsByTagName('h2')[0];
        let value = a.innerHTML || a.innerText || a.textContent;
        if (value.toLowerCase().indexOf(search) > -1) {
            center[i].style.display = '';
        }
        else {
            center[i].style.display = 'none';
        }
    }
}