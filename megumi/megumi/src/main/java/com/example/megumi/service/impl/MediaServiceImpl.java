package com.example.megumi.service.impl;

import com.example.megumi.model.Media;
import com.example.megumi.repository.MediaRepository;
import com.example.megumi.service.MediaService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class MediaServiceImpl implements MediaService {

    @Autowired
    private MediaRepository mediaRepository;

    @Override
    public List<Media> getAllMedia() {
        return mediaRepository.findAll();
    }

    @Override
    public Optional<Media> getMediaById(Long id) {
        return mediaRepository.findById(id);
    }

    @Override
    public Media save(Media media) {
        return mediaRepository.save(media);
    }

    @Override
    public void delete(Long id) {
        mediaRepository.deleteById(id);
    }
}
