# Driving School Management System (DSMS) 

## Overview: 

A DSMS is a tool that streamlines the planning, organization, and execution of driving lessons. In my version, the application will manage lessons, instructors, and students. Authenticated users will be able to create, manipulate, search, and browse records in a SQL server database. 

 

## Database Design and Tables: 

The application will consist of 3 main tables:  

- Students Table: This will be managing the events in the application 

- Instructors Table: This will manage the different instructors 

- Lessons Table: This table will represent the different lessons that a student can have. Each lesson must be taught by 1 instructor 

The application may also have other tables like the login and cars tables. 

## Features: 

Authenticated users will be able to: 

- Create, Manipulate, Search and Browse and Manipulate records 

- Enroll students in lessons  

- Manage instructors 
 
- Cancel, reschedule lessons. 

## Business Rules 

- Students must be at least 16 years old to enroll in driving lessons 

- Lessons with the same instructor must not have overlapping dates AND times. 

- A student cannot have more than 1 lesson scheduled on the same day 

- If a lesson is to be rescheduled or cancelled, this must be done at least 1-day (24) hours before the lesson, if not a fee will be added to the students due. (Fee amount to  be decided later).  