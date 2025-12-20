/*
  # Create Cellular Company Database Schema

  1. New Tables
    - `clients`
      - `id` (uuid, primary key) - unique identifier for the client
      - `full_name` (text) - client's full name
      - `phone` (text) - client's phone number
      - `email` (text) - client's email address
      - `registration_date` (timestamptz) - when the client registered
      - `created_at` (timestamptz) - record creation timestamp
    
    - `plans`
      - `id` (uuid, primary key) - unique identifier for the tariff plan
      - `name` (text) - plan name
      - `description` (text) - plan description
      - `price` (decimal) - monthly price
      - `minutes` (integer) - included minutes
      - `internet_gb` (integer) - included internet in gigabytes
      - `sms` (integer) - included SMS messages
      - `status` (text) - plan status (active, inactive, archived)
      - `created_at` (timestamptz) - record creation timestamp
    
    - `contracts`
      - `id` (uuid, primary key) - unique identifier for the contract
      - `client_id` (uuid, foreign key) - reference to client
      - `plan_id` (uuid, foreign key) - reference to tariff plan
      - `contract_number` (text) - unique contract number
      - `start_date` (date) - contract start date
      - `end_date` (date) - contract end date
      - `status` (text) - contract status (active, suspended, terminated)
      - `created_at` (timestamptz) - record creation timestamp

  2. Security
    - Enable RLS on all tables
    - Add policies for authenticated users to perform all CRUD operations
    - This is a mobile application where authenticated users can manage all records

  3. Important Notes
    - All tables use UUID as primary key with automatic generation
    - Foreign key constraints ensure referential integrity
    - Timestamps are automatically set on creation
    - Status fields use text type for flexibility
*/

-- Create clients table
CREATE TABLE IF NOT EXISTS clients (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  full_name text NOT NULL,
  phone text NOT NULL,
  email text NOT NULL,
  registration_date timestamptz DEFAULT now(),
  created_at timestamptz DEFAULT now()
);

-- Create plans table
CREATE TABLE IF NOT EXISTS plans (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  name text NOT NULL,
  description text DEFAULT '',
  price decimal(10, 2) NOT NULL,
  minutes integer DEFAULT 0,
  internet_gb integer DEFAULT 0,
  sms integer DEFAULT 0,
  status text DEFAULT 'active',
  created_at timestamptz DEFAULT now()
);

-- Create contracts table
CREATE TABLE IF NOT EXISTS contracts (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  client_id uuid NOT NULL REFERENCES clients(id) ON DELETE CASCADE,
  plan_id uuid NOT NULL REFERENCES plans(id) ON DELETE RESTRICT,
  contract_number text NOT NULL UNIQUE,
  start_date date NOT NULL,
  end_date date,
  status text DEFAULT 'active',
  created_at timestamptz DEFAULT now()
);

-- Create indexes for better query performance
CREATE INDEX IF NOT EXISTS idx_contracts_client_id ON contracts(client_id);
CREATE INDEX IF NOT EXISTS idx_contracts_plan_id ON contracts(plan_id);
CREATE INDEX IF NOT EXISTS idx_contracts_contract_number ON contracts(contract_number);

-- Enable Row Level Security
ALTER TABLE clients ENABLE ROW LEVEL SECURITY;
ALTER TABLE plans ENABLE ROW LEVEL SECURITY;
ALTER TABLE contracts ENABLE ROW LEVEL SECURITY;

-- Policies for clients table
CREATE POLICY "Authenticated users can view all clients"
  ON clients FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert clients"
  ON clients FOR INSERT
  TO authenticated
  WITH CHECK (true);

CREATE POLICY "Authenticated users can update clients"
  ON clients FOR UPDATE
  TO authenticated
  USING (true)
  WITH CHECK (true);

CREATE POLICY "Authenticated users can delete clients"
  ON clients FOR DELETE
  TO authenticated
  USING (true);

-- Policies for plans table
CREATE POLICY "Authenticated users can view all plans"
  ON plans FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert plans"
  ON plans FOR INSERT
  TO authenticated
  WITH CHECK (true);

CREATE POLICY "Authenticated users can update plans"
  ON plans FOR UPDATE
  TO authenticated
  USING (true)
  WITH CHECK (true);

CREATE POLICY "Authenticated users can delete plans"
  ON plans FOR DELETE
  TO authenticated
  USING (true);

-- Policies for contracts table
CREATE POLICY "Authenticated users can view all contracts"
  ON contracts FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert contracts"
  ON contracts FOR INSERT
  TO authenticated
  WITH CHECK (true);

CREATE POLICY "Authenticated users can update contracts"
  ON contracts FOR UPDATE
  TO authenticated
  USING (true)
  WITH CHECK (true);

CREATE POLICY "Authenticated users can delete contracts"
  ON contracts FOR DELETE
  TO authenticated
  USING (true);