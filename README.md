# HotelBookingAPI

The purpose of this API is to provide the following functionality
- Create Bookings
- Modify Bookings: I have assumed that you can only modify the booking dates
- Cancel Bookings
- Availability

Each of them are behing an HTTP endpoint. They can be tested through any HTTP requesting tool, but additionally it does provide a SWAGGER endpoint to run them.

# Business rules
- API will be maintained by the hotel’s IT department.
- As it’s the very last hotel, the quality of service must be 99.99 to 100% => no downtime
- For the purpose of the test, we assume the hotel has only one room available
- To give a chance to everyone to book the room, the stay can’t be longer than 3 days and can’t be reserved more than 30 days in advance.
- All reservations start at least the next day of booking,
- To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.
- Every end-user can check the room availability, place a reservation, cancel it or modify it.
- To simplify the API is insecure.