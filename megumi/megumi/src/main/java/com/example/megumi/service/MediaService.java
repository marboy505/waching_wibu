package com.example.megumi.service;

import com.example.megumi.model.Media;
import java.util.List;
import java.util.Optional;

public interface MediaService {
    List<Media> getAllMedia();
    Optional<Media> getMediaById(Long id);
    Media save(Media media);
    void delete(Long id);
}
