let loginBtn = document.getElementById('login-btn');

loginBtn.addEventListener('click', () => {
  let email = document.getElementById('loginU').value.toString().trim();
  let password = document.getElementById('loginP').value.toString().trim();

  if (email && password) {
    axios.post('http://localhost:5032/api/login', {
      email: email,
      password: password
    })
    .then((response) => {
      console.log(response.data);
      // redirect to a dashboard  successful login
      window.location = 'dashboard.html';
    })
    .catch((error) => {
      console.error(error);
      alert('Incorrect username or password');
    });
  } else {
    alert('Please enter a username and password');
  }
});
