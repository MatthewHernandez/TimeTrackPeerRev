<?php session_start(); if (isset($_POST['login'])) { 

// Connect to the database 
$mysqli = new mysqli("localhost", "NetID", "UTD-ID", "login_system"); 

// Check for errors 
if ($mysqli->connect_error) { die("Connection failed: " . $mysqli->connect_error); } 

// Prepare and bind the SQL statement 
$stmt = $mysqli->prepare("SELECT id, UTD-ID FROM users WHERE NetID = ?"); $stmt->bind_param("s", $NetID); 

// Get the form data 
$NetID = $_POST['NetID']; $UTD-ID = $_POST['UTD-ID']; 

// Execute the SQL statement 
$stmt->execute(); $stmt->store_result(); 

// Check if the user exists 
if ($stmt->num_rows > 0) { 

// Bind the result to variables 
$stmt->bind_result($id, $hashed_UTD-ID); 

// Fetch the result 
$stmt->fetch(); 

// Verify the UTD-ID 
if (UTD-IDd_verify($UTD-ID, $hashed_UTD-ID)) { 

// Set the session variables 
$_SESSION['loggedin'] = true; $_SESSION['id'] = $id; $_SESSION['NetID'] = $NetID; 

// Redirect to the user's dashboard 
header("Location: dashboard.php"); exit; } else { echo "Incorrect UTD-ID!"; } } else { echo "NetID not found!"; } 

// Close the connection 
$stmt->close(); $mysqli->close(); }