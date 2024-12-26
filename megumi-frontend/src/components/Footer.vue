<template>
  <footer class="footer">
    <p>&copy; 2024 メグミ. All Rights Reserved.</p>
    <div class="social-media">
      <a href="#" aria-label="Facebook" class="social-icon">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="#" aria-label="Twitter" class="social-icon">
        <i class="fab fa-twitter"></i>
      </a>
      <a href="#" aria-label="Instagram" class="social-icon">
        <i class="fab fa-instagram"></i>
      </a>
    </div>
  </footer>
</template>

<style scoped>
.footer {
  background-color: #141414;
  color: white;
  padding: 20px;
  text-align: center;
  box-shadow: 0 -2px 4px rgba(0, 0, 0, 0.8);
}

.social-media {
  margin-top: 10px;
}

.social-icon {
  display: inline-block;
  background-color: #e50914;
  color: white;
  width: 24px;
  height: 24px;
  line-height: 24px;
  text-align: center;
  border-radius: 50%;
  margin: 0 5px;
  text-decoration: none;
  font-weight: bold;

  display: inline-flex;
  justify-content: center;
  align-items: center;
  width: 50px; /* Lebar lingkaran */
  height: 50px; /* Tinggi lingkaran */
  border-radius: 50%; /* Membuat elemen menjadi lingkaran */
  color: white; /* Warna ikon */
  font-size: 24px; /* Ukuran ikon */
  /* margin: 0 10px; Spasi antar ikon */
  transition: background-color 0.3s ease; /* Animasi saat hover */
}

.social-icon:hover {
  background-color: #f40612;
  color: #fff; /* Warna ikon saat di-hover */
}
</style>
