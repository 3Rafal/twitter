#!/bin/bash

# Wait for PostgreSQL database to be ready
# Usage: ./wait-for-db.sh [max_retries] [retry_interval]

set -e

MAX_RETRIES=${1:-30}
RETRY_INTERVAL=${2:-2}
RETRY_COUNT=0

echo "Waiting for database to be ready..."

while [ $RETRY_COUNT -lt $MAX_RETRIES ]; do
    if docker-compose exec -T db pg_isready -U twitter_user -d twitter_clone > /dev/null 2>&1; then
        echo "Database is ready!"
        exit 0
    fi

    RETRY_COUNT=$((RETRY_COUNT + 1))
    echo "Waiting for database... (attempt $RETRY_COUNT/$MAX_RETRIES)"
    sleep $RETRY_INTERVAL
done

echo "Database failed to start after $MAX_RETRIES attempts"
echo "Database logs:"
docker-compose logs db
exit 1