# Yoga Studio Attendance API — Deployment Fork

This is a fork of [n01570640/YogaStudioAttendanceAPI](https://github.com/n01570640/YogaStudioAttendanceAPI) configured for cloud deployment on Render.

## Live Demo

**Deployed API URL:** https://yogastudioattendanceapi.onrender.com

Called by the deployed main app for clock in / clock out endpoints.

## How this fork differs from the original

- Migrated from Oracle → PostgreSQL
- Added Dockerfile for container deployment
- Added deployed main app URL to CORS allowed origins
- Clock in / out timestamps stored as UTC (display layer converts to Eastern)

## For the original project setup

See the [original repository](https://github.com/n01570640/YogaStudioAttendanceAPI) — this fork is not intended for local development.
