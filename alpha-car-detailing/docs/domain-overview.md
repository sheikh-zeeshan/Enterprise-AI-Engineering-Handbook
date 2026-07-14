# Alpha Car Detailing Domain Overview

## Business summary

Alpha Car Detailing operates vehicle washing and detailing stations across the country. It supports immediate walk-in services and contract-based work for corporate fleets, government departments, insurance companies, and rental companies.

## Initial capability areas

- Station and service-catalog management
- Customer and organization accounts
- Fleet and vehicle management
- Booking and walk-in job intake
- Work-order and bay scheduling
- Pricing, contracts, discounts, and approvals
- Payments, invoicing, and account billing
- Notifications and integration events
- Identity, authorization, audit, and compliance
- Reporting, observability, and operational support

## Initial bounded-context candidates

- Customer Management
- Fleet Management
- Booking and Intake
- Service Operations
- Pricing and Contracts
- Billing and Payments
- Identity and Access
- Notifications
- Reporting and Analytics

These boundaries are candidates, not final microservice decisions. The architecture chapters will validate ownership, coupling, transaction boundaries, workload characteristics, and team topology before implementation boundaries are frozen.
