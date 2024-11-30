<%@ Page Title="Time Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="tt_pr.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .calendar {
            width: 100%;
            border-collapse: collapse;
        }
        .calendar th, .calendar td {
            padding: 15px;
            text-align: center;
        }
        .calendar th {
            background-color: #f2f2f2;
        }
        .calendar td {
            border: none;
        }
        .description-textarea {
            width: 100%;
            box-sizing: border-box;
        }
    </style>
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        <p>Time spent:</p>
        <input id="Text1" type="text" />
        <p>Description:</p>
        <textarea id="TextArea1" class="description-textarea" rows="4" maxlength="140" oninput="updateCounter()"></textarea>
        <p><span id="charCount">0</span> / 140</p>
        <p></p>
        <input id="Submit1" type="submit" value="Submit" />
    </main>
    <script type="text/javascript">
        var selectedDate = null;

        function updateCounter() {
            var textarea = document.getElementById('TextArea1');
            var charCount = document.getElementById('charCount');
            charCount.textContent = textarea.value.length;
        }

        function updateSelectedDate(date) {
            selectedDate = date;
        }

        document.getElementById('Submit1').addEventListener('click', function (event) {
            var timeSpent = document.getElementById('Text1').value;
            var description = document.getElementById('TextArea1').value;

            var today = new Date();
            var pastWeek = new Date();
            pastWeek.setDate(today.getDate() - 7);
            var selectedDateObj = new Date(selectedDate);

            var timePattern = /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/;

            if (selectedDateObj < pastWeek || selectedDateObj > today || !selectedDate) {
                alert('You must select a date within the past 7 days');
                event.preventDefault();
                return;
            } else if (!timePattern.test(timeSpent)) {
                alert('Time spent must be in HH:MM format');
                event.preventDefault();
                return;
            } else if (timeSpent === "00:00" || timeSpent === "0:00") {
                alert('Time spent must be a positive value');
                event.preventDefault();
                return;
            } else if (description.length < 1) {
                alert('You must submit a description');
                event.preventDefault();
                return;
            } else {
                alert('Submission received');
            }
        });

        document.getElementById('<%= Calendar1.ClientID %>').addEventListener('change', function () {
            var selectedDateValue = this.value;
            updateSelectedDate(selectedDateValue);
        });
    </script>
</asp:Content>
