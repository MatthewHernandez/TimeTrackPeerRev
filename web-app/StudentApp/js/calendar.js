//THIS IS NOT NEEDED FOR WEBPAGE DUE TO MOVING CALENDAR JS TO DASHBOARD.JS, BUT LEAVING FOR NOW FOR DEBUG


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

    

}

prev.addEventListener('click', () => {
    currentDate.setMonth(currentDate.getMonth() - 1);
    updateCalendar();
})

next.addEventListener('click', () => {
    currentDate.setMonth(currentDate.getMonth() + 1);
    updateCalendar();
})

updateCalendar();

// modal input section

const close = document.getElementById("close");
const open = document.getElementById("date");
const modal = document.getElementById("modal");

open.addEventListener('click', () => modal.classList.add('show-modal'));

close.addEventListener('click', () => modal.classList.remove('show-modal'));