# Event Management System Project

## Overview: 

An Event is a tool that streamlines the planning, organization, and execution of events. In my version, the application will manage events, attendees, and the sessions within each event. Authenticated users will be able to create, manipulate, search, and browse records in a SQL server database. 

 

## Database Design and Tables: 

The application will consist of 4 main tables:  

- Events Table: This will be managing the events in the application 

- Attendees Table: This will manage the different attendees (general users) 

- Sessions Table: This table will manage the different sessions that an Event can have 

- Event-Attendees Table: This table will represent the many-to-many relationships between the events and attendees 

The application may also have other tables like the login table. 

## Features: 

Authenticated users will be able to: 

- Create Records 

- Manipulate Records 

- Search Records 

- Browse Records 

- Manage Events: Register attendees, manage sessions... 

## Business Rules 

- Each event must have a unique combination of name and dates 

- Sessions with the same event must not have overlapping times. 