
let btn = document.getElementById('btn');



btn.addEventListener('click', () => {
    let u = document.getElementById('u').value.toString().trim();
    let p = document.getElementById('p').value.toString().trim();
    let e = document.getElementById('e').value.toString().trim();
    let cp = document.getElementById('cp').value.toString().trim();


  if (u && e && p && (p === cp)) {
   
   
   

    axios.post('http://localhost:5032/api/users', {
     username: u,
     email: e,
     password: p
})
  .then((response) => {
    console.log(response.data);
    window.location = 'dashboard.html'
  })
  .catch((error) => {
    console.error(error);
  });

  } else {
    alert('Something went wrong with sign up.');
  
    
 
  }
  console.log(`u=${u}, e=${e}, p=${p}, cp=${cp}`);
});
