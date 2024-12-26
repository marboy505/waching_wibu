package com.example.megumi.service;

import com.example.megumi.model.Content;
import java.util.List;
import java.util.Optional;

public interface ContentService {
    List<Content> getAllContent();
    Optional<Content> getContentById(Long id);
    Content save(Content content);
    void delete(Long id);
}
