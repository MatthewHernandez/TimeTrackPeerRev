// Get all navigation links, the content container, and the header title
const navLinks = document.querySelectorAll('.nav-link');
const contentDiv = document.getElementById('content');
const headerTitle = document.getElementById('header-title');

navLinks.forEach(link => {

    // Add click event listener for each link
    link.addEventListener('click', (event) => {

        // Get the new title
        const title = link.getAttribute('data-title');

        // Get the target html page
        const target = link.getAttribute('data-target');

        // Prevent the page from reloading
        event.preventDefault();

        // Remove active state from all links
        navLinks.forEach(nav => nav.classList.remove('active'));

        // Add active state to current link
        link.classList.add('active');

        // Set header title
        headerTitle.textContent = title;

        // Fetch the target HTML file and insert it into the content div
        fetch(target)
            .then(response => {  // Check network response
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(html => {
                contentDiv.innerHTML = html; // Insert the html page into the content div

                // If the calendar page is loaded, initialize the calendar directly
                if (target === 'calendar.html') {
                    initializeCalendar();  // Call a function to initialize the calendar logic
                }

                // Get the button (if on the peer form page)
                const button = contentDiv.querySelector('.submit-button');

                // If there is a submit button on the page (peer review page), listen for click
                if (button) {
                    button.addEventListener('click', submissionMessage);
                }
            })
            .catch(error => {
                contentDiv.innerHTML = `<p>Error loading content</p>`;  // Display error message if content couldn't be loaded
            });
    });
});

// Replace content when the submit button is clicked on peer review form
function submissionMessage() {
    fetch('submission.html')
        .then(response => { // Check network response
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(html => {
            contentDiv.innerHTML = html; // Insert the html page into the content div
        })
        .catch(error => {
            contentDiv.innerHTML = `<p>Error loading content</p>`; // Display error message if content couldn't be loaded
        });
}

// Function to initialize the calendar (called every time calendar.html is loaded)
function initializeCalendar() {
    const monthYear = document.getElementById('monthYear');
const dates = document.getElementById('dates');
const prev = document.getElementById('prev');
const next = document.getElementById('next');

let currentDate = new Date();

const updateCalendar = () => {
    const currentYear = currentDate.getFullYear();
    const currentMonth = currentDate.getMonth();

    const firstDay = new Date(currentYear, currentMonth, 0);
    const lastDay = new Date(currentYear, currentMonth + 1, 0);
    const totalDays = lastDay.getDate();
    const firstDayIndex = firstDay.getDay();
    const lastDayIndex = lastDay.getDay();

    const monthYearString = currentDate.toLocaleString('default', {month: 'long', year: 'numeric'});
    monthYear.textContent = monthYearString;

    let datesHTML = '';

    for(let i = firstDayIndex; i > 0; i--) {
        const prevDate = new Date(currentYear, currentMonth, 1 - i);
        datesHTML += `<div class = "date inactive">${prevDate.getDate()}</div>`;
    }

    for(let i = 1; i <= totalDays; i++) {
        const date = new Date(currentYear, currentMonth, i);
        const activeClass = date.toDateString() === new Date().toDateString() ? 'active' : '';
        datesHTML += `<div class = "date ${activeClass}">${i}</div>`;
    }

    for(let i = 1; i <= 7 - lastDayIndex; i++) {
        const nextDate = new Date(currentYear, currentMonth + 1, i);
        datesHTML += `<div class = "date inactive">${nextDate.getDate()}</div>`;
    }

    dates.innerHTML = datesHTML;
    };

    // Event listeners for navigating between months
    prev.addEventListener('click', () => {
        currentDate.setMonth(currentDate.getMonth() - 1);
        updateCalendar();
    });

    next.addEventListener('click', () => {
        currentDate.setMonth(currentDate.getMonth() + 1);
        updateCalendar();
    });

    // Initial calendar setup
    updateCalendar();
}
