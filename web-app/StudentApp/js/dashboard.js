// Get all navigation links, the content container, and the header title
const navLinks = document.querySelectorAll('.nav-link');
const contentDiv = document.getElementById('content');
const headerTitle = document.getElementById('header-title'); 


navLinks.forEach(link => {

    //Add click event listener for each link
    link.addEventListener('click', (event) => { 

        // Get the new title 
        const title = link.getAttribute('data-title'); 

        // Get the target html page 
        const target = link.getAttribute('data-target'); 

    

        // Prevent the page from reloading
        event.preventDefault(); 

        //Remove active state from all links
        navLinks.forEach(nav => nav.classList.remove('active'));

        //Add active state to current link
        link.classList.add('active');

        //Set header title
        headerTitle.textContent = title; 


        // Fetch the target HTML file and insert it into the content div
        fetch(target)
            .then(response => {  //check network response
                if (!response.ok) {
                    throw new Error('Network response was not ok'); 
                }
                return response.text();
            })
            .then(html => {
                contentDiv.innerHTML = html; // Insert the html page into the content div
                
                // Get the button (if on the peer form page)
                const button = contentDiv.querySelector('.submit-button');
                
                // If there is a submit button on the page (peer review page), listen for click
                if (button) {
                    button.addEventListener('click', submissionMessage);
                }
            })
            .catch(error => {
                contentDiv.innerHTML = `<p>Error loading content</p>`;  // Display error message if content couldnt be loaded
            });
    });
});


// Replace content when the submit button is clicked on peer review form
function submissionMessage() {
    fetch('submission.html') 
        .then(response => { //check network response
            if (!response.ok) {
                throw new Error('Network response was not ok'); 
            }
            return response.text(); 
        })
        .then(html => {
            contentDiv.innerHTML = html; // Insert the html page into the content div
        })
        .catch(error => {
            contentDiv.innerHTML = `<p>Error loading content</p>`; // Display error message if content couldnt be loaded
        });
  }
  
