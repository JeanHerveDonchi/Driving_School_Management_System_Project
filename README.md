# Driving School Management System (DSMS) 

## Overview: 

A DSMS is a tool that streamlines the planning, organization, and execution of theoretical and practical driving lessons. In my version, the application will manage lessons, instructors, and lessons. Authenticated users will be able to create, manipulate, search, and browse records in a SQL server database. 

 

## Database Design and Tables: 

The application will consist of 5 main tables:  

- Students Table: This will be managing the events in the application 

- Instructors Table: This will manage the different attendees (general users) 

- Packages Table: This table will manage the different packages that a student can enroll in 

- Enrollment (Student-Packages): This table will represent the many-to-many relationships between the student and packages. 

- Lessons Table: This table will represent the different lessons that an enrollment can have. Each lesson must be taught by 1 instructor 

The application may also have other tables like the login table. 

## Features: 

Authenticated users will be able to: 

Create, Manipulate, Search and Browse and Manipulate records 

- Enroll students in packages  

- Manage lesson instructors 
 
- Cancel, reschedule lessons. 

## Business Rules 

- Students must be at least 16 years old to enroll in driving lessons 

- Lessons with the same instructor must not have overlapping dates AND times. 

- Students must have driver's license class 7.1 to qualify for some packages 

- A student cannot have more than 1 lesson scheduled on the same day 

- If a lesson is to be rescheduled or cancelled, this must be done at least 1-day (24) hours before the lesson, if not a fee will be added to the students due. (Fee amount to be decided later). 