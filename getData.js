let btn = document.getElementById('edit');
btn.addEventListener('click', ()=>{
 let u = document.getElementById('u').value.toString().trim();
 let e = document.getElementById('e').value.toString().trim();

 if(u && e){
     axios.post('http://localhost:5032/api/edit', {
                 
                 username : u,
                 email : e
              })
                .then((response) => {

                   console.log(response.data);
                   alert("your username has been changed!");
               })
                .catch((error) => {
                 console.error(error);
              });
 }
 else{
     alert("error");
 }
       
});

const currentDate = new Date();
const year = currentDate.getFullYear();
const month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
const day = ('0' + currentDate.getDate()).slice(-2);
const formattedDate = year + '-' + month + '-' + day;
console.log(formattedDate); // e.g. 2023-03-17

let add = document.getElementById('add');
add.addEventListener('click', () =>{
 let amount = document.getElementById('amount').value.toString().trim();
 let sub = document.getElementById('sub').value.toString().trim();
 if(amount){
     axios.post('http://localhost:5032/api/expenses', {
                 
         date : formattedDate,
         amount : amount,
         subject : sub
              })
                .then((response) => {

                   console.log(response.data);
                   alert("your data inserted!");
               })
                .catch((error) => {
                 console.error(error);
              });
 }
 else {
     alert("error");
 }
});
//----------------------------------------------GET DATA For table --------------------------------------------------//
function getDataToTable(){
      axios.get('http://localhost:5032/api/expenses')
      .then((response) => {
        // const expenses = response.data;
        // const expensesArray = [];
        const expenses = response.data;
        const table = document.getElementById('myTable');
        const thead = table.getElementsByTagName('thead')[0];
        
        for (let i = 0; i < expenses.length; i++) {
          const tr = document.createElement('tr');
          tr.innerHTML = `
            <td>${expenses[i].date}</td>
            <td>${expenses[i].amount}</td>
            <td>${expenses[i].subject}</td>
            <td>${expenses[i].amount_70}</td>
            <td>${expenses[i].amount_30}</td>
          `;
          thead.appendChild(tr);
        }
    
        console.log(response.data); // log the response data to the console
        })
     .catch((error) => {
       console.error(error); // log any errors to the console
       });

       
}
getDataToTable();

//------------------------------------------------ GET DATA For GRAPH -----------------------------------------------//


function getDataToChart(){
    axios.get('http://localhost:5032/api/expenses')
.then((response) => {
const expenses = response.data;

// Process data to group by month and calculate total amount
const monthlyTotals = {};
expenses.forEach((expense) => {
const month = expense.date.split('-')[1];
if (monthlyTotals[month]) {
monthlyTotals[month] += expense.amount;
} else {
monthlyTotals[month] = expense.amount;
}
});

// Create chart using Chart.js
const ctx = document.getElementById('myChart').getContext('2d');
const myChart = new Chart(ctx, {
type: 'bar',
data: {
labels: Object.keys(monthlyTotals),
datasets: [{
label: 'Total Amount per Month',
data: Object.values(monthlyTotals),
backgroundColor: 'rgba(54, 162, 235, 0.2)',
borderColor: 'rgba(54, 162, 235, 1)',
borderWidth: 1
}]
},
options: {
scales: {
y: {
beginAtZero: true
}
}
}
});
})
.catch((error) => {
console.error(error);
});

}
getDataToChart();
//-------------------------------- TABLE TO EXCEL -------------------------------------//
function export_to_exell() {
    TableToExcel.convert(document.getElementById("myTable"));
}




