// Function to toggle the sidebar
function toggleSidebar() {
    var sidebar = document.getElementById("sidebar");
    sidebar.classList.toggle("active");
  }
  
  // Function to open the Account modal
  function openAccountPopup() {
    fetch('Account.html')
      .then(response => response.text())
      .then(data => {
        document.getElementById("accountContent").innerHTML = data;
        document.getElementById("accountModal").style.display = "block";
      })
      .catch(error => console.error("Error loading Account.html:", error));
  }
  
  // Function to close the Account modal
  function closeAccountPopup() {
    document.getElementById("accountModal").style.display = "none";
  }
  