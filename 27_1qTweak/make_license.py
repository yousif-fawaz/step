import sys, jwt, datetime

# Usage: python make_license.py <HW_FINGERPRINT> [days_valid]
# Prints JWT to stdout

def main():
	if len(sys.argv) < 2:
		print("Usage: python make_license.py <HW_FINGERPRINT> [days_valid]", file=sys.stderr)
		sys.exit(1)
	hw = sys.argv[1]
	days = int(sys.argv[2]) if len(sys.argv) > 2 else 365
	with open("private.pem", "rb") as f:
		key = f.read()
	payload = {
		"sub": "customer@example.com",
		"plan": "pro",
		"hw": hw,
		"exp": int((datetime.datetime.utcnow() + datetime.timedelta(days=days)).timestamp())
	}
	print(jwt.encode(payload, key, algorithm="RS256"))

if __name__ == "__main__":
	main()